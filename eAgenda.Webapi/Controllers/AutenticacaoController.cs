using AutoMapper;
using eAgenda.Aplicacao.ModuloAutenticacao;
using eAgenda.Dominio.ModuloAutenticacao;
using eAgenda.Webapi.Controllers.Compartilhado;
using eAgenda.Webapi.ViewModels.ModuloAutenticacao;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.Webapi.Controllers
{
    [Route("api/conta")]
    [ApiController]
    public class AutenticacaoController : eAgendaControllerBase
    {
        private readonly ServicoAutenticacao servicoAutenticacao;
        private readonly IMapper mapeadorUsuario;

        public AutenticacaoController(ServicoAutenticacao servicoAutenticacao, IMapper mapeadorUsuario)
        {
            this.servicoAutenticacao = servicoAutenticacao;
            this.mapeadorUsuario = mapeadorUsuario;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult> RegistrarUsuario(RegistrarUsuarioViewModel usuarioVM)
        {
            var usuario = mapeadorUsuario.Map<Usuario>(usuarioVM);

            var usuarioResult = await servicoAutenticacao.RegistrarUsuario(usuario, usuarioVM.Senha);

            if (usuarioResult.IsFailed)
                return InternalError(usuarioResult);

            return Ok(new
            {
                sucesso = true,
                dados = usuarioResult.Value
            });
        }


        [HttpPost("autenticar")]
        public async Task<ActionResult> AutenticarUsuario(AutenticarUsuarioViewModel usuarioVM)
        {
            var usuarioResult = await servicoAutenticacao.AutenticarUsuario(usuarioVM.Email, usuarioVM.Senha);

            if (usuarioResult.IsFailed)
                return BadRequest(usuarioResult);

            return Ok(new
            {
                sucesso = true,
                dados = GerarJwt(usuarioResult.Value)
            });
        }

        [HttpPost("sair")]
        public async Task<ActionResult> Logout()
        {
            await servicoAutenticacao.Sair();

            return Ok();
        }

        private TokenViewModel GerarJwt(Usuario usuario)
        {
            var identityClaims = new ClaimsIdentity();

            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()));
            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Email, usuario.Email));
            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.GivenName, usuario.Nome));

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("SegredoSuperSecretoDoeAgenda");
            DateTime dataExpiracao = DateTime.UtcNow.AddHours(8);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = "eAgenda",
                Audience = "http://localhost",
                Subject = identityClaims,
                Expires = dataExpiracao,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            var response = new TokenViewModel
            {
                Chave = encodedToken,
                DataExpiracacao = dataExpiracao,
                UsuarioToken = new UsuarioTokenViewModel
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email
                }
            };

            return response;
        }
    }
}
