using AutoMapper;
using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Infra.Orm;
using eAgenda.Infra.Orm.ModuloContato;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Config.AutoMapperConfig;
using WebApplication1.ViewModels.ModuloContato;

namespace WebApplication1.Controllers
{
    [Route("api/contatos")]
    [ApiController]
    public partial class ContatoController : ControllerBase
    {
        private ServicoContato servicoContato;
        private IMapper mapeador;
        public ContatoController(ServicoContato servicoContato, IMapper mapeador)
        {
            //IConfiguration configuracao = new ConfigurationBuilder()
            //  .SetBasePath(Directory.GetCurrentDirectory())
            //  .AddJsonFile("appsettings.json")
            //  .Build();

            //var connectionString = configuracao.GetConnectionString("SqlServer");

            //var builder = new DbContextOptionsBuilder<eAgendaDbContext>();

            //builder.UseSqlServer(connectionString);

            //var contextoPersistencia = new eAgendaDbContext(builder.Options);

            //var repositorioContato = new RepositorioContatoOrm(contextoPersistencia);

            //servicoContato = new ServicoContato(repositorioContato, contextoPersistencia);

            this.servicoContato = servicoContato;
            this.mapeador = mapeador;
        }

        [HttpGet]
        public List<ListarContatoViewModel> SelecioarTodos(StatusFavoritoEnum status)
        {
            var contatos = servicoContato.SelecionarTodos(status).Value;

            return mapeador.Map<List<ListarContatoViewModel>>(contatos);
        }

        [HttpGet("visualizacao-completa/{id}")]
        public VisualizarContatoViewModel SelecionarPorId(Guid id)
        {
            var contato = servicoContato.SelecionarPorId(id).Value;

            var contatoViewModel = mapeador.Map<VisualizarContatoViewModel>(contato);

            return contatoViewModel;
        }

        [HttpPost]
        public string Inserir(InserirContatoViewModel contatoViewModel)
        {
            var contato = mapeador.Map<Contato>(contatoViewModel);

            var resultado = servicoContato.Inserir(contato);

            if(resultado.IsSuccess)
                return "Contato inserido com sucesso!";

            string[] erros = resultado.Errors.Select(x => x.Message).ToArray();
            
            return string.Join("\r\n ", erros);
        }

        [HttpPut("{id}")]
        public string Editar(Guid id, InserirContatoViewModel contatoViewModel)
        {
            var contatoEncontrado = servicoContato.SelecionarPorId(id).Value;

            var contatoAlterado = mapeador.Map(contatoViewModel, contatoEncontrado);

            var resultado = servicoContato.Editar(contatoAlterado);

            if (resultado.IsSuccess)
                return "Contato editado com sucesso!";

            string[] erros = resultado.Errors.Select(x => x.Message).ToArray();

            return string.Join("\r\n ", erros);
        }

        [HttpDelete]
        public string Excluir(Guid id)
        {
            var resultadoBusca = servicoContato.SelecionarPorId(id);

            List<string> erros = new List<string>();

            if(resultadoBusca.IsFailed)
            {
                 erros = resultadoBusca.Errors.Select(x => x.Message).ToList();

                return string.Join("\r\n ", erros);
            }

            var contato = resultadoBusca.Value;

            var resultado = servicoContato.Excluir(contato);

            if (resultado.IsSuccess)
                return "Contato excluido com sucesso!";

            erros = resultadoBusca.Errors.Select(x => x.Message).ToList();

            return string.Join("\r\n ", erros);
        }
    }
}
