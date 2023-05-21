using eAgenda.Dominio.ModuloCompromisso;
using System;

namespace eAgenda.Webapi.ViewModels.ModuloCompromisso
{
    public class FormsCompromissoViewModel
    {
        public Guid id { get; set; }

        public string Assunto { get; set; }

        public string Local { get; set; }

        public TipoLocalizacaoCompromissoEnum TipoLocal { get; set; }

        public string Link { get; set; }

        public DateTime Data { get; set; }

        public TimeSpan HoraInicio { get; set; }

        public TimeSpan HoraTermino { get; set; }

        public Guid ContatoId { get; set; }

    }

}
