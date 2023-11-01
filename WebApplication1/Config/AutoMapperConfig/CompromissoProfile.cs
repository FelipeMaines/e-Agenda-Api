using eAgenda.Dominio.ModuloCompromisso;
using Microsoft.EntityFrameworkCore.Query.Internal;
using WebApplication1.Controllers;

namespace WebApplication1.Config.AutoMapperConfig
{
    public class CompromissoProfile : Profile
    {
        public CompromissoProfile()
        {
            CreateMap<Compromisso, ListarCompromissoViewModel>()
               .ForMember(des => des.Data, opt => opt.MapFrom(origem => origem.Data.ToShortDateString()))
               .ForMember(des => des.HoraInicio, opt => opt.MapFrom(origem => origem.HoraInicio.ToString(@"hh\:mm")))
               .ForMember(des => des.HoraTermino, opt => opt.MapFrom(origem => origem.HoraTermino.ToString(@"hh\:mm")));
        }
    }
}
