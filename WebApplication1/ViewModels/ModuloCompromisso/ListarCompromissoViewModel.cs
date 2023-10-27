namespace WebApplication1.Controllers
{
    public partial class ContatoController
    {
        public class ListarCompromissoViewModel
        {
            public Guid Id { get; set; }
            public string Assunto { get; set; }
            public DateTime Data { get; set; }
            public TimeSpan HoraInicio { get; set; }
            public TimeSpan HoraTermino { get; set; }
        }
    }
}
