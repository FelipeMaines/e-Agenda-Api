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
        private IMapper mapeador;

        public TarefaController(ServicoTarefa servicoTarefa, IMapper mapeador)
        {
            this.mapeador = mapeador;
            this.servicoTarefa = servicoTarefa;
        }

        [HttpGet]
        public List<ListarTarefaViewModel> SelecionarTodos()
        {
            var tarefas = servicoTarefa.SelecionarTodos(StatusTarefaEnum.Pendentes).Value;

            return mapeador.Map<List<ListarTarefaViewModel>>(tarefas);
        }

        [HttpGet("{id}")]
        public VisualizarTarefaViewModel SelecionarPorId(Guid id)
        {
            var tarefa = servicoTarefa.SelecionarPorId(id).Value;

            //var itens = new List<ListarItensViewModel>();

            //foreach (var item in tarefa.Itens)
            //    itens.Add(new ListarItensViewModel(item.Id, item.Titulo, item.Concluido));

            return mapeador.Map<VisualizarTarefaViewModel>(tarefa);
        }

        [HttpPost]
        public string Inserir(FormTarefaViewModel tarefaForm)
        {
            var tarefa = mapeador.Map<Tarefa>(tarefaForm);

            var result = servicoTarefa.Inserir(tarefa);

            var erros = new List<string>();

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

            var result = servicoTarefa.Editar(mapeador.Map(tarefaForm, tarefa));

            var erros = new List<string>();

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
