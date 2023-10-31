using eAgenda.Aplicacao.ModuloDespesa;
using eAgenda.Aplicacao.ModuloTarefa;
using eAgenda.Infra.Orm.ModuloDespesa;
using eAgenda.Infra.Orm;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eAgenda.Infra.Orm.ModuloTarefa;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using eAgenda.Dominio.ModuloTarefa;
using WebApplication1.ViewModels.ModuloTarefa;

namespace WebApplication1.Controllers.ModuloTarefa
{
    [Route("api/tarefas")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private ServicoTarefa servicoTarefa;

        public TarefaController()
        {
            IConfiguration configuracao = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();

            var connectionString = configuracao.GetConnectionString("SqlServer");

            var builder = new DbContextOptionsBuilder<eAgendaDbContext>();

            builder.UseSqlServer(connectionString);

            var contextoPersistencia = new eAgendaDbContext(builder.Options);

            var repositorioTarefa = new RepositorioTarefaOrm(contextoPersistencia);

            servicoTarefa = new ServicoTarefa(repositorioTarefa, contextoPersistencia);
        }

        [HttpGet]
        public List<ListarTarefaViewModel> teste()
        {
            var tarefa = servicoTarefa.SelecionarTodos(StatusTarefaEnum.Pendentes).Value;

            var tarefasViewModels = new List<ListarTarefaViewModel>();

            foreach (var item in tarefa)
                tarefasViewModels.Add(new ListarTarefaViewModel(item.Id, item.Titulo, item.Prioridade, item.DataCriacao, item.DataConclusao, item.PercentualConcluido));

            return tarefasViewModels;
        }

        [HttpGet("{id}")]
        public VisualizarTarefaViewModel SelecionarPorId(Guid id)
        {
            var tarefa = servicoTarefa.SelecionarPorId(id).Value;

            var itens = new List<ListarItensViewModel>();

            foreach (var item in tarefa.Itens)
                itens.Add(new ListarItensViewModel(item.Id, item.Titulo, item.Concluido));

            return new VisualizarTarefaViewModel(tarefa.Id, tarefa.Titulo, tarefa.Prioridade, tarefa.DataCriacao, tarefa.DataConclusao, tarefa.PercentualConcluido, itens);
        }

        [HttpPost]
        public string Inserir(FormTarefaViewModel tarefaForm)
        {
            var itens = new List<ItemTarefa>();

            foreach (var item in tarefaForm.itens)
            {
                itens.Add(new ItemTarefa(item.Titulo));
            }

            var tarefa = new Tarefa(itens, tarefaForm.Titulo, tarefaForm.Prioridade);

            var erros = new List<string>();

            var result = servicoTarefa.Inserir(tarefa);

            if (result.IsFailed)
            {
                erros = result.Errors.Select(e => e.Message).ToList();

                return string.Format("\r\n", erros);
            }

            return "Inserido com sucesso";
        }

        [HttpPut("{id}")]
        public string Editar(Guid id, FormTarefaViewModel tarefaForm)
        {
            var tarefa = servicoTarefa.SelecionarPorId(id).Value;

            var items = new List<ItemTarefa>();

            foreach(var item in tarefaForm.itens)
            {
                items.Add(new ItemTarefa(item.Titulo));
            }

            tarefa.Atualizar(new Tarefa(items, tarefaForm.Titulo, tarefaForm.Prioridade));

            var erros = new List<string>();

            var result = servicoTarefa.Editar(tarefa);

            if (result.IsFailed)
            {
                erros = result.Errors.Select(e => e.Message).ToList();

                return string.Format("\r\n", erros);
            }

            return "Editado com sucesso";
        }

        [HttpDelete]
        public string Excluir(Guid id)
        {
            var erros = new List<string>();

            var result = servicoTarefa.Excluir(id);

            if (result.IsFailed)
            {
                erros = result.Errors.Select(e => e.Message).ToList();

                return string.Format("\r\n", erros);
            }

            return "Excluido com sucesso";
        }
    }
}
