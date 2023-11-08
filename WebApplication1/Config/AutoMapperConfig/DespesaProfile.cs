using eAgenda.Dominio.ModuloDespesa;
using System.IdentityModel.Tokens.Jwt;
using WebApplication1.ViewModels.ModuloDespesa;
using AutoMapper.Extensions.EnumMapping;


namespace WebApplication1.Config.AutoMapperConfig
{
    public class DespesaProfile : Profile
    {
        public DespesaProfile()
        {
            CreateMap<Despesa, ListarDespesaViewModel>();
            CreateMap<Despesa, VisualizarDespesaViewModel>();
            CreateMap<Despesa, FormsDespesaViewModel>();
            CreateMap<FormsDespesaViewModel, Despesa>();
        }
    }
}
