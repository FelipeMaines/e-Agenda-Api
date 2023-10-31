using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Infra.Orm.ModuloContato;
using eAgenda.Infra.Orm;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static WebApplication1.Controllers.ContatoController;
using WebApplication1.ViewModels.ModuloContato;
using eAgenda.Aplicacao.ModuloDespesa;
using eAgenda.Infra.Orm.ModuloDespesa;
using WebApplication1.ViewModels.ModuloDespesa;
using eAgenda.Dominio.ModuloDespesa;
using WebApplication1.ViewModels.ModuloCategoria;
using System.Reflection.Metadata.Ecma335;

namespace WebApplication1.Controllers.ModuloDespesa
{
    [Route("api/despesas")]
    [ApiController]
    public class DespesaController : ControllerBase
    {
        private ServicoDespesa servicoDespesa;
        private ServicoCategoria servicoCategoria;
        public DespesaController()
        {
            IConfiguration configuracao = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();

            var connectionString = configuracao.GetConnectionString("SqlServer");

            var builder = new DbContextOptionsBuilder<eAgendaDbContext>();

            builder.UseSqlServer(connectionString);

            var contextoPersistencia = new eAgendaDbContext(builder.Options);

            var repositorioDespesa = new RepositorioDespesaOrm(contextoPersistencia);

            var repositorioCategoria = new RepositorioCategoriaOrm(contextoPersistencia);

            servicoDespesa = new ServicoDespesa(repositorioDespesa, contextoPersistencia);

            servicoCategoria = new ServicoCategoria(repositorioCategoria, contextoPersistencia);
        }

        [HttpGet]
        public List<ListarDespesaViewModel> SelecioarTodos()
        {
            var despesas = servicoDespesa.SelecionarTodos().Value;

            var despesasView = new List<ListarDespesaViewModel>();

            foreach (var despesa in despesas)
            {
                var despesaViewModel = new ListarDespesaViewModel(despesa.Id, despesa.Descricao,
                    despesa.Valor, despesa.Data, despesa.FormaPagamento);

                despesasView.Add(despesaViewModel);
            }

            return despesasView;
        }

        [HttpGet("{id}")]
        public VisualizarDespesaViewModel SelecionarPorId(Guid id)
        {
            var despesa = servicoDespesa.SelecionarPorId(id).Value;

            var categorias = new List<ListarCategoriaViewModel>();

            foreach(var cate in despesa.Categorias)
            {
                categorias.Add(new ListarCategoriaViewModel(cate.Titulo, cate.Id));
            }

            var despesasView = new List<ListarDespesaViewModel>();

            var despesView = new VisualizarDespesaViewModel(id,despesa.Descricao, despesa.Valor, despesa.Data,
               despesa.FormaPagamento, categorias);

            return despesView;
        }

        [HttpPost]
       public string Inserir(FormsDespesaViewModel despesaView)
        {
            List<Categoria> categorias = despesaView.Categorias.Select(id => servicoCategoria.SelecionarPorId(id).Value).ToList();

            var despesa = new Despesa(despesaView.Descricao, despesaView.Valor, despesaView.Data, despesaView.FormaPagamento, categorias);

            var result = servicoDespesa.Inserir(despesa);

            var erros = new List<string>();

            if(result.IsFailed)
            {
                erros = result.Errors.Select(e => e.Message).ToList();

                return string.Format("\r\n", erros);
            }

            return "Inserido com sucesso";
        }

        [HttpPut("{id}")]
        public string Editar(Guid id,FormsDespesaViewModel despesaView)
        {
            var despesa = servicoDespesa.SelecionarPorId(id).Value;

            despesa.Valor = despesaView.Valor;
            despesa.Descricao = despesaView.Descricao;
            despesa.FormaPagamento = despesaView.FormaPagamento;
            despesa.Categorias = despesaView.Categorias.Select(id => servicoCategoria.SelecionarPorId(id).Value).ToList();
            despesa.Data = despesaView.Data;

            var result = servicoDespesa.Editar(despesa);

            var erros = new List<string>();

            if (result.IsFailed)
            {
                erros = result.Errors.Select(e => e.Message).ToList();

                return string.Format("\r\n", erros);
            }

            return "Editado com sucesso";
        }

        [HttpDelete("{id}")]
        public string Excluir(Guid id)
        {
            var despesa = servicoDespesa.SelecionarPorId(id).Value;

            var result = servicoDespesa.Excluir(despesa);

            var erros = new List<string>();

            if (result.IsFailed)
            {
                erros = result.Errors.Select(x => x.Message).ToList();

                return string.Format("\r\n", erros);
            }

            else
                return "Excluido com sucesso!";
        }
    }
}
