using System.ComponentModel;

namespace eAgenda.Dominio.ModuloDespesa
{
    public enum FormaPgtoDespesaEnum
    {        
        PIX, 
        
        Dinheiro,

        [Description("Cartão de Crédito")]
        CartaoCredito
    }

}
