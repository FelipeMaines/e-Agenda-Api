using System.Text.Json;
using WebApplication1.ViewModels.ModuloCategoria;

namespace WebApplication1.Config
{
    public class ManipuladorExcecoes
    {
        private readonly RequestDelegate requestDelegate;

        public ManipuladorExcecoes(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext ctx)
        {
            try
            {
                await this.requestDelegate(ctx);
            }
            catch (Exception ex)
            {
                ctx.Response.StatusCode = 500;
                ctx.Response.ContentType = "application/json";

                var problem = new
                {
                    Sucesso = false,
                    Erros = new List<string> { ex.Message }
                };

                ctx.Response.WriteAsync(JsonSerializer.Serialize(problem));
            }
        }
    }
}
