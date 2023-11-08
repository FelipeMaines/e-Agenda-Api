using eAgenda.Dominio.ModuloTarefa;
using Microsoft.EntityFrameworkCore.Query.Internal;
using WebApplication1.ViewModels.ModuloTarefa;

namespace WebApplication1.Config.AutoMapperConfig
{
    public class TarefaProfile : Profile
    {
        public TarefaProfile()
        {
            CreateMap<Tarefa, ListarTarefaViewModel>();
            CreateMap<Tarefa, FormTarefaViewModel>();
            CreateMap<Tarefa, VisualizarTarefaViewModel>();
            CreateMap<FormTarefaViewModel, Tarefa>();
            CreateMap<ItemTarefa, ListarItensViewModel>();
        }
    }
}
