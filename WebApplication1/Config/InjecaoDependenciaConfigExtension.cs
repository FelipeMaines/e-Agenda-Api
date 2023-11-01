using eAgenda.Aplicacao.ModuloCompromisso;
using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Aplicacao.ModuloDespesa;
using eAgenda.Aplicacao.ModuloTarefa;
using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Dominio.ModuloTarefa;
using eAgenda.Dominio;
using eAgenda.Infra.Orm.ModuloCompromisso;
using eAgenda.Infra.Orm.ModuloContato;
using eAgenda.Infra.Orm.ModuloDespesa;
using eAgenda.Infra.Orm.ModuloTarefa;
using eAgenda.Infra.Orm;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Config
{
    public static class InjecaoDependenciaConfigExtension
    {
        public static void ConfigurarInjecaoDependecia(this IServiceCollection service, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("SqlServer");

            service.AddDbContext<IContextoPersistencia, eAgendaDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(connectionString);
            });

            service.AddTransient<IRepositorioTarefa, RepositorioTarefaOrm>();
            service.AddTransient<ServicoTarefa>();

            service.AddTransient<IRepositorioContato, RepositorioContatoOrm>();
            service.AddTransient<ServicoContato>();

            service.AddTransient<IRepositorioCompromisso, RepositorioCompromissoOrm>();
            service.AddTransient<ServicoCompromisso>();

            service.AddTransient<IRepositorioDespesa, RepositorioDespesaOrm>();
            service.AddTransient<ServicoDespesa>();

            service.AddTransient<IRepositorioCategoria, RepositorioCategoriaOrm>();
            service.AddTransient<ServicoCategoria>();
        }
    }
}
