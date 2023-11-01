using Microsoft.EntityFrameworkCore.Query.Internal;

namespace WebApplication1.Config
{
    public static class SwaggerConfiguration
    {
        public static void ConfigurarSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}
