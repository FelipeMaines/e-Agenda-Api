using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Infra.Orm;
using eAgenda.Infra.Orm.ModuloContato;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.ViewModels.ModuloContato;

namespace WebApplication1.Controllers
{
    [Route("api/contatos")]
    [ApiController]
    public partial class ContatoController : ControllerBase
    {
        private ServicoContato servicoContato;
        public ContatoController()
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

            servicoContato = new ServicoContato(repositorioContato, contextoPersistencia);
        }

        [HttpGet]
        public List<ListarContatoViewModel> SelecioarTodos(StatusFavoritoEnum status)
        {
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
        public VisualizarContatoViewModel SelecionarPorId(Guid id)
        {
            var contato = servicoContato.SelecionarPorId(id).Value;

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

        [HttpPost]
        public string Inserir(InserirContatoViewModel contatoViewModel)
        {
            var contato = new Contato(
                contatoViewModel.Nome,
                contatoViewModel.Email,
                contatoViewModel.Telefone,
                contatoViewModel.Empresa,
                contatoViewModel.Cargo);

            var resultado = servicoContato.Inserir(contato);

            if(resultado.IsSuccess)
                return "Contato inserido com sucesso!";

            string[] erros = resultado.Errors.Select(x => x.Message).ToArray();
            
            return string.Join("\r\n ", erros);
        }

        [HttpPut("{id}")]
        public string Editar(Guid id, EditarContatoViewModel contatoViewModel)
        {
            var contato = servicoContato.SelecionarPorId(id).Value;

            contato.Nome = contatoViewModel.Nome;
            contato.Email = contatoViewModel.Email;
            contato.Telefone = contatoViewModel.Telefone;
            contato.Cargo = contatoViewModel.Cargo;
            contato.Empresa = contatoViewModel.Empresa;

            var resultado = servicoContato.Editar(contato);

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
