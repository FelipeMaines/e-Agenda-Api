using eAgenda.Dominio.ModuloTarefa;

namespace WebApplication1.ViewModels.ModuloTarefa
{
    public class FormTarefaViewModel
    {
        public string Titulo { get; set; }
        public PrioridadeTarefaEnum Prioridade { get; set; }
        public List<FormItemsViewModel> itens { get; set; }

    }
}
