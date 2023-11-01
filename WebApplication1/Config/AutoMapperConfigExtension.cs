using WebApplication1.Config.AutoMapperConfig;

namespace WebApplication1.Config
{
    public static class AutoMapperConfigExtension
    {
        public static void ConfigurarAutoMapper(this IServiceCollection Services)
        {
            Services.AddAutoMapper(opt =>
            {
                opt.AddProfile<ContatoProfile>();
                opt.AddProfile<CompromissoProfile>();
            });
        }
    }
}
