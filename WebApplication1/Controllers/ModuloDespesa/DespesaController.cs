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
        private IMapper mapeador;
        public DespesaController(ServicoDespesa servicoDespesa, ServicoCategoria servicoCategoria, IMapper mapeador)
        {
            this.servicoCategoria = servicoCategoria;
            this.servicoDespesa = servicoDespesa;
            this.mapeador = mapeador;
        }

        [HttpGet]
        public List<ListarDespesaViewModel> SelecioarTodos()
        {
            var despesas = servicoDespesa.SelecionarTodos().Value;

            return this.mapeador.Map<List<ListarDespesaViewModel>>(despesas);
        }

        [HttpGet("{id}")]
        public VisualizarDespesaViewModel SelecionarPorId(Guid id)
        {
            var despesa = servicoDespesa.SelecionarPorId(id).Value;

            return mapeador.Map<VisualizarDespesaViewModel>(despesa);
        }

        [HttpPost]
       public string Inserir(FormsDespesaViewModel despesaView)
        {
            var despesa = mapeador.Map<Despesa>(despesaView);

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

            var despesaEditada = mapeador.Map(despesaView, despesa);

            var result = servicoDespesa.Editar(despesaEditada);

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
