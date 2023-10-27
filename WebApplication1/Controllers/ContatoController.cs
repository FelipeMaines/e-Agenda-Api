using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Infra.Orm;
using eAgenda.Infra.Orm.ModuloContato;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    [Route("api/contatos")]
    [ApiController]
    public partial class ContatoController : ControllerBase
    {
        [HttpGet]
        public List<ListarContatoViewModel> SelecioarTodos(StatusFavoritoEnum status)
        {
            IConfiguration configuracao = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();

            var connectionString = configuracao.GetConnectionString("SqlServer");

            var builder = new DbContextOptionsBuilder<eAgendaDbContext>();

            builder.UseSqlServer(connectionString);

            var contextoPersistencia = new eAgendaDbContext(builder.Options);
            var repositorioContato = new RepositorioContatoOrm(contextoPersistencia);
            var servicoContato = new ServicoContato(repositorioContato, contextoPersistencia);

            var contatos = servicoContato.SelecionarTodos(status).Value;

            var contatosViewModel = new List<ListarContatoViewModel>();

            foreach(var contato in contatos)
            {
                var contatoViewModel = new ListarContatoViewModel
                {
                    Id = contato.Id,
                    Nome = contato.Nome,
                    Email = contato.Email,
                    Telefone = contato.Telefone,
                    Empresa = contato.Empresa,
                    Cargo = contato.Cargo
                };

                contatosViewModel.Add(contatoViewModel);
            }


            return contatosViewModel;
        }

        [HttpGet("visualizacao-completa/{id}")]
        public VisualizarContatoViewModel SelecionarPorId(string id)
        {
            IConfiguration configuracao = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();

            var connectionString = configuracao.GetConnectionString("SqlServer");

            var builder = new DbContextOptionsBuilder<eAgendaDbContext>();

            builder.UseSqlServer(connectionString);

            var contextoPersistencia = new eAgendaDbContext(builder.Options);
            var repositorioContato = new RepositorioContatoOrm(contextoPersistencia);
            var servicoContato = new ServicoContato(repositorioContato, contextoPersistencia);

            var contato = servicoContato.SelecionarPorId(Guid.Parse(id)).Value;

            var compromissos = new List<ListarCompromissoViewModel>();

            foreach (var compromisso in contato.Compromissos)
            {
                compromissos.Add(new ListarCompromissoViewModel
                {
                    Id = compromisso.Id,
                    Assunto = compromisso.Assunto,
                    Data = compromisso.Data,
                    HoraInicio = compromisso.HoraInicio,
                    HoraTermino = compromisso.HoraTermino
                });
            }

            return new VisualizarContatoViewModel
            {
                Id = contato.Id,
                Nome = contato.Nome,
                Email = contato.Email,
                Telefone = contato.Telefone,
                Empresa = contato.Empresa,
                Cargo = contato.Cargo,
                Compromissos = compromissos
            };
        }
    }
}
