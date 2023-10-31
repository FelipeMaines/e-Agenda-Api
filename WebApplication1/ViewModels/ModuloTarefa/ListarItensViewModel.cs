namespace WebApplication1.ViewModels.ModuloTarefa
{
    public class ListarItensViewModel
    {
        public Guid Id { get; set; }

        public string Titulo { get; set; }

        public bool Concluido { get; set; }

        public ListarItensViewModel(Guid id, string titulo, bool concluido)
        {
            Id = id;
            Titulo = titulo;
            Concluido = concluido;
        }
    }
}
