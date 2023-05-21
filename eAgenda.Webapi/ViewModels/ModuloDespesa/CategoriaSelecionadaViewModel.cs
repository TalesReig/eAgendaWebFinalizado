using System;

namespace eAgenda.Webapi.ViewModels.ModuloDespesa
{
    public class CategoriaSelecionadaViewModel
    {
        public Guid Id { get; set; }

        public string Titulo { get; set; }

        public bool Selecionada { get; set; }
    }

}
