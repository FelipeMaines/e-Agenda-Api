using eAgenda.Dominio.ModuloDespesa;
using WebApplication1.ViewModels.ModuloCategoria;

namespace WebApplication1.ViewModels.ModuloDespesa
{
    public class VisualizarDespesaViewModel
    {
        public VisualizarDespesaViewModel(Guid id, string descricao, decimal valor, DateTime data,
            FormaPgtoDespesaEnum formaPagamento, List<ListarCategoriaViewModel> categorias)
        {
            Id = id;
            Descricao = descricao;
            Valor = valor;
            Data = data;
            FormaPagamento = formaPagamento;
            Categorias = categorias;
        }

        public Guid Id { get; set; }
        public string Descricao { get; set; }

        public decimal Valor { get; set; }

        public DateTime Data { get; set; }

        public FormaPgtoDespesaEnum FormaPagamento { get; set; }

        public List<ListarCategoriaViewModel> Categorias { get; set; }
    }
}
