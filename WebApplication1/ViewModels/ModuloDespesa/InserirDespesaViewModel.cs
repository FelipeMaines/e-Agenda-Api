using eAgenda.Dominio.ModuloDespesa;

namespace WebApplication1.ViewModels.ModuloDespesa
{
    public class InserirDespesaViewModel
    {
        public InserirDespesaViewModel(string descricao, decimal valor, DateTime data, FormaPgtoDespesaEnum formaPagamento, Guid[] categorias)
        {
            Descricao = descricao;
            Valor = valor;
            Data = data;
            FormaPagamento = formaPagamento;
            Categorias = categorias;
        }

        public string Descricao { get; set; }

        public decimal Valor { get; set; }

        public DateTime Data { get; set; }

        public FormaPgtoDespesaEnum FormaPagamento { get; set; }
        public Guid[] Categorias { get; set; }
    }
}
