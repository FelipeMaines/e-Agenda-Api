using eAgenda.Dominio.ModuloTarefa;

namespace WebApplication1.ViewModels.ModuloTarefa
{
    public class VisualizarTarefaViewModel
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public PrioridadeTarefaEnum Prioridade { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataConclusao { get; set; }
        public decimal PercentualConcluido { get; set; }
        public List<ListarItensViewModel> itens {get; set;}

        public VisualizarTarefaViewModel(
            Guid id,
            string titulo,
            PrioridadeTarefaEnum prioridade,
            DateTime dataCriacao,
            DateTime? dataConclusao,
            decimal percentualConcluido,
            List<ListarItensViewModel> itens)
        {
            Id = id;
            Titulo = titulo;
            Prioridade = prioridade;
            DataCriacao = dataCriacao;
            DataConclusao = dataConclusao;
            PercentualConcluido = percentualConcluido;
            this.itens = itens;
        }
    }
}
