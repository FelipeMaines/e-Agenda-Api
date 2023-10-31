using eAgenda.Dominio.ModuloContato;
using static WebApplication1.Controllers.ContatoController;

namespace WebApplication1.Controllers
{
    public class VizualarCompromissoViewModel
    {
        public Guid Id { get; set; }
        public string Assunto {get; set;}
        public string? Local  {get; set;}
        public string? Link { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }
        public ListarContatoViewModel Contato {get; set;}
    }

}
