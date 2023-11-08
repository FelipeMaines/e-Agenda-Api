using eAgenda.Dominio.ModuloDespesa;
using WebApplication1.ViewModels.ModuloCategoria;

namespace WebApplication1.Config.AutoMapperConfig
{
    public class CategoriaProfile : Profile
    {

        public CategoriaProfile()
        {
            CreateMap<Categoria, ListarCategoriaViewModel>();
            CreateMap<Categoria, VisualizarCategoriaViewModel>();
            CreateMap<Categoria, FormsCategoriaViewModel>();
            CreateMap<FormsCategoriaViewModel, Categoria>();
        }
    }
}
