using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace WebApplication1.Filters
{
    public class SerilogActionFilter : IActionFilter
    {
        private object nomeEndpoint;
        private object nomeModulo;
        public string NomeEndpoint
        {
            get
            {
                return this.nomeEndpoint.ToString()!
                    .SepararPalavrasPorMaiusculas();
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception == null)
            {
                Log.Logger.Information($"[Módulo de {nomeModulo}] -> {NomeEndpoint} executado com sucesso");
            }
            else if (context.Exception != null)
            {
                Log.Logger.Error($"[Módulo de {nomeModulo}] -> Falha ao executar {NomeEndpoint}");
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            nomeEndpoint = context.RouteData.Values["action"];

            nomeModulo = context.RouteData.Values["controller"];

            Log.Logger.Information($"[Módulo de {nomeModulo}] -> Tentando {NomeEndpoint}...");
        }
    }
}
