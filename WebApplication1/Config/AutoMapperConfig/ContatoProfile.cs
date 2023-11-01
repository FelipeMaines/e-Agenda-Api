
using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Dominio.ModuloContato;
using Microsoft.EntityFrameworkCore.Query.Internal;
using static WebApplication1.Controllers.ContatoController;
using WebApplication1.Controllers;
using WebApplication1.ViewModels.ModuloContato;

namespace WebApplication1.Config.AutoMapperConfig
{
    public class ContatoProfile : Profile
    {
        public ContatoProfile()
        {
             CreateMap<Contato, ListarContatoViewModel>();
             CreateMap<Contato, VisualizarContatoViewModel>();
             CreateMap<InserirContatoViewModel, Contato>();
        }
    }
}
