using AutoMapper;
using eAgenda.Aplicacao.ModuloCompromisso;
using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Webapi.Controllers.Compartilhado;
using eAgenda.Webapi.ViewModels.ModuloCompromisso;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace eAgenda.Webapi.Controllers
{
    [Route("api/compromissos")]
    [ApiController]
    [Authorize]
    public class CompromissoController : eAgendaControllerBase
    {
        private readonly ServicoCompromisso servicoCompromisso;
        private readonly IMapper mapeadorCompromissos;

        public CompromissoController(ServicoCompromisso servicoCompromisso, IMapper mapeadorCompromissos)
        {
            this.servicoCompromisso = servicoCompromisso;
            this.mapeadorCompromissos = mapeadorCompromissos;
        }

        [HttpGet]
        public ActionResult<List<ListarCompromissoViewModel>> SelecionarTodos()
        {
            var compromissoResult = servicoCompromisso.SelecionarTodos();

            if (compromissoResult.IsFailed)
                return InternalError(compromissoResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorCompromissos.Map<List<ListarCompromissoViewModel>>(compromissoResult.Value)
            });
        }

        [HttpGet, Route("entre/{dataInicial:datetime}/{dataFinal:datetime}")]
        public ActionResult<List<ListarCompromissoViewModel>> SelecionarCompromissosFuturos(DateTime dataInicial, DateTime dataFinal)
        {
            var compromissoResult = servicoCompromisso.SelecionarCompromissosFuturos(dataInicial, dataFinal);

            if (compromissoResult.IsFailed)
                return InternalError(compromissoResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorCompromissos.Map<List<ListarCompromissoViewModel>>(compromissoResult.Value)
            });
        }

        [HttpGet, Route("passados/{dataAtual:datetime}")]
        public ActionResult<List<ListarCompromissoViewModel>> SelecionarCompromissosPassados(DateTime dataAtual)
        {
            var compromissoResult = servicoCompromisso.SelecionarCompromissosPassados(dataAtual);

            if (compromissoResult.IsFailed)
                return InternalError(compromissoResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorCompromissos.Map<List<ListarCompromissoViewModel>>(compromissoResult.Value)
            });
        }

        [HttpGet("visualizacao-completa/{id:guid}")]
        public ActionResult<VisualizarCompromissoViewModel> SelecionarCompromissoCompletoPorId(Guid id)
        {
            var compromissoResult = servicoCompromisso.SelecionarPorId(id);

            if (compromissoResult.IsFailed && RegistroNaoEncontrado(compromissoResult))
                return NotFound(compromissoResult);

            if (compromissoResult.IsFailed)
                return InternalError(compromissoResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorCompromissos.Map<VisualizarCompromissoViewModel>(compromissoResult.Value)
            });
        }

        [HttpGet("{id:guid}")]
        public ActionResult<FormsCompromissoViewModel> SelecionarCompromissoPorId(Guid id)
        {
            var compromissoResult = servicoCompromisso.SelecionarPorId(id);

            if (compromissoResult.IsFailed && RegistroNaoEncontrado(compromissoResult))
                return NotFound(compromissoResult);

            if (compromissoResult.IsFailed)
                return InternalError(compromissoResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorCompromissos.Map<FormsCompromissoViewModel>(compromissoResult.Value)
            });
        }

        [HttpPost]
        public ActionResult<FormsCompromissoViewModel> Inserir(FormsCompromissoViewModel compromissoVM)
        {
            var compromisso = mapeadorCompromissos.Map<Compromisso>(compromissoVM);

            var compromissoResult = servicoCompromisso.Inserir(compromisso);

            if (compromissoResult.IsFailed)
                return InternalError(compromissoResult);

            return Ok(new
            {
                sucesso = true,
                dados = compromissoVM
            });
        }

        [HttpPut("{id:guid}")]
        public ActionResult<FormsCompromissoViewModel> Editar(Guid id, FormsCompromissoViewModel compromissoVM)
        {
            var compromissoResult = servicoCompromisso.SelecionarPorId(id);

            if (compromissoResult.IsFailed && RegistroNaoEncontrado(compromissoResult))
                return NotFound(compromissoResult);

            var compromisso = mapeadorCompromissos.Map(compromissoVM, compromissoResult.Value);

            compromissoResult = servicoCompromisso.Editar(compromisso);

            if (compromissoResult.IsFailed)
                return InternalError(compromissoResult);

            return Ok(new
            {
                sucesso = true,
                dados = compromissoVM
            });
        }

        [HttpDelete("{id:guid}")]
        public ActionResult Excluir(Guid id)
        {
            var compromissoResult = servicoCompromisso.Excluir(id);

            if (compromissoResult.IsFailed && RegistroNaoEncontrado<Compromisso>(compromissoResult))
                return NotFound<Compromisso>(compromissoResult);

            if (compromissoResult.IsFailed)
                return InternalError<Compromisso>(compromissoResult);

            return NoContent();
        }
    }
}
