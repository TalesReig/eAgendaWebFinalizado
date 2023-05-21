using System.Collections.Generic;

namespace eAgenda.Webapi.ViewModels.ModuloDespesa
{
    public class VisualizarCategoriaViewModel
    {
        public string Titulo { get; set; }

        public List<ListarDespesaViewModel> Despesas { get; set; }

    }
}
