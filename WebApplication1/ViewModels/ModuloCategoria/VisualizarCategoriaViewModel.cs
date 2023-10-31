using WebApplication1.ViewModels.ModuloDespesa;

namespace WebApplication1.ViewModels.ModuloCategoria
{
    public class VisualizarCategoriaViewModel
    {
        public Guid Id { get; set; }
        public string Titulo { get; set;}
        public List<ListarDespesaViewModel> Despesas { get; set; }

        public VisualizarCategoriaViewModel(Guid id, string titulo, List<ListarDespesaViewModel> despesas)
        {
            Id = id;
            Titulo = titulo;
            Despesas = despesas;
        }
    }
}
