using eAgenda.Aplicacao.ModuloCompromisso;
using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Infra.Orm.ModuloCompromisso;
using eAgenda.Infra.Orm.ModuloContato;
using eAgenda.Infra.Orm;
using Microsoft.EntityFrameworkCore;
using eAgenda.Aplicacao.ModuloDespesa;
using eAgenda.Infra.Orm.ModuloDespesa;
using WebApplication1.ViewModels.ModuloCategoria;
using Microsoft.AspNetCore.Mvc;
using eAgenda.Dominio.ModuloDespesa;
using WebApplication1.ViewModels.ModuloDespesa;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using FluentResults;

namespace WebApplication1.Controllers.ModuloCategoria
{
    [Route("api/categorias")]
    [ApiController]
    public class CategoriaController
    {
        private ServicoCategoria servicoCategoria;
        private ServicoDespesa servicoDespesa;
        public CategoriaController()
        {
            IConfiguration configuracao = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();

            var connectionString = configuracao.GetConnectionString("SqlServer");

            var builder = new DbContextOptionsBuilder<eAgendaDbContext>();

            builder.UseSqlServer(connectionString);

            var contextoPersistencia = new eAgendaDbContext(builder.Options);

            var repositorioCategoria = new RepositorioCategoriaOrm(contextoPersistencia);
            var repositorioDespesa = new RepositorioDespesaOrm(contextoPersistencia);

            servicoCategoria = new ServicoCategoria(repositorioCategoria, contextoPersistencia);
            servicoDespesa = new ServicoDespesa(repositorioDespesa, contextoPersistencia);
        }
        [HttpGet]
        public List<ListarCategoriaViewModel> SelecionarTodos()
        {
            var categorias = servicoCategoria.SelecionarTodos().Value;

            var categoriasViewModel = new List<ListarCategoriaViewModel>();

            foreach (var categoria in categorias)
            {
                var categoriaView = new ListarCategoriaViewModel(categoria.Titulo, categoria.Id);

                categoriasViewModel.Add(categoriaView);
            }

            return categoriasViewModel;
        }

        [HttpGet("{id}")]
        public VisualizarCategoriaViewModel SelecioarPorId(Guid id)
        {
            var categoria = servicoCategoria.SelecionarPorId(id).Value;

            var despesas = new List<ListarDespesaViewModel>();

            foreach (var despesa in categoria.Despesas)
            {
                var despesaView = new ListarDespesaViewModel(despesa.Id, despesa.Descricao, despesa.Valor, despesa.Data, despesa.FormaPagamento);

                despesas.Add(despesaView);
            }

            return new VisualizarCategoriaViewModel(categoria.Id, categoria.Titulo, despesas);
        }

        [HttpPost]
        public string Inserir(FormsCategoriaViewModel categoriaForm)
        {
            var categoria = new Categoria(categoriaForm.Titulo);

            var result = servicoCategoria.Inserir(categoria);

            var erros = new List<string>();

            if (result.IsFailed)
            {
                erros = result.Errors.Select(x => x.Message).ToList();

                return string.Format("\r\n", erros);
            }

            else
                return "Inserido com sucesso!";
        }

        [HttpPut("{id}")]
        public string Editar(Guid id, FormsCategoriaViewModel categoriaView)
        {
            var categoria = servicoCategoria.SelecionarPorId(id).Value;

            categoria.Titulo = categoriaView.Titulo;

            var result = servicoCategoria.Editar(categoria);

            var erros = new List<string>();


            if (result.IsFailed)
            {
                erros = result.Errors.Select(x => x.Message).ToList();

                return string.Format("\r\n", erros);
            }

            else
                return "Editado com sucesso!";
        }

        [HttpDelete("{id}")]
        public string Excluir(Guid id)
        {
            var result = servicoCategoria.Excluir(id);

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
