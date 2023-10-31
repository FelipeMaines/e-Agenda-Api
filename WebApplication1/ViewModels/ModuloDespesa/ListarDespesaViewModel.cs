using eAgenda.Dominio.ModuloDespesa;

namespace WebApplication1.ViewModels.ModuloDespesa
{
    public class ListarDespesaViewModel
    {
        public ListarDespesaViewModel(Guid id, string descricao, decimal valor, DateTime data, FormaPgtoDespesaEnum formaPagamento)
        {
            Id = id;
            Descricao = descricao;
            Valor = valor;
            Data = data;
            FormaPagamento = formaPagamento;
        }

        public Guid Id { get; set; }
        public string Descricao { get; set; }

        public decimal Valor { get; set; }

        public DateTime Data { get; set; }

        public FormaPgtoDespesaEnum FormaPagamento { get; set; }
    }
}
