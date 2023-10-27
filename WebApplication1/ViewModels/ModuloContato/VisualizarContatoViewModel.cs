namespace WebApplication1.Controllers
{
    public partial class ContatoController
    {
        public class VisualizarContatoViewModel
        {
            public VisualizarContatoViewModel()
            {
                Compromissos = new List<ListarCompromissoViewModel>();
            }

            public Guid Id { get; set; }
            public string Nome { get; set; }
            public string Email { get; set; }
            public string Telefone { get; set; }
            public string Empresa { get; set; }
            public string Cargo { get; set; }

            public List<ListarCompromissoViewModel> Compromissos { get; set; }
        }
    }
}
