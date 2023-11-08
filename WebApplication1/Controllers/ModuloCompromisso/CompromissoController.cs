using eAgenda.Aplicacao.ModuloCompromisso;
using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Infra.Orm.ModuloContato;
using eAgenda.Infra.Orm;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eAgenda.Infra.Orm.ModuloCompromisso;
using static WebApplication1.Controllers.ContatoController;
using eAgenda.Dominio.ModuloCompromisso;
using WebApplication1.ViewModels.ModuloContato;
using eAgenda.Dominio.ModuloContato;
using WebApplication1.ViewModels.ModuloCompromisso;

namespace WebApplication1.Controllers.ModuloCompromisso
{
    [Route("api/compromissos")]
    [ApiController]
    public class CompromissoController : ControllerBase
    {
        private ServicoCompromisso servicoCompromisso;
        private ServicoContato servicoContato;
        private IMapper mapeador;

        public CompromissoController(ServicoCompromisso servicoCompromisso, ServicoContato servicoContato, IMapper mapeador)
        {
            this.mapeador = mapeador;
            this.servicoCompromisso = servicoCompromisso;
            this.servicoContato = servicoContato;
        }

        [HttpGet]
        public List<ListarCompromissoViewModel> SelecionarTodos()
        {
            var compromissos = servicoCompromisso.SelecionarTodos().Value;

            return mapeador.Map<List<ListarCompromissoViewModel>>(compromissos);
        }

        [HttpGet("{id}")]
        public VizualarCompromissoViewModel SelecionarPorId(Guid id)
        {
            var compromisso = servicoCompromisso.SelecionarPorId(id).Value;

            var compromissoView = mapeador.Map<VizualarCompromissoViewModel>(compromisso);

            //var compromissoView = new VizualarCompromissoViewModel
            //{
            //    Assunto = compromisso.Assunto,
            //    Data = compromisso.Data,
            //    HoraInicio = compromisso.HoraInicio,
            //    HoraTermino = compromisso.HoraTermino,
            //    Local = compromisso.Local,
            //    Link = compromisso.Link,
            //    Contato = new ListarContatoViewModel
            //    {
            //        Id = compromisso.Contato.Id,
            //        Nome = compromisso.Contato.Nome,
            //        Email = compromisso.Contato.Email,
            //        Telefone = compromisso.Contato.Telefone,
            //        Empresa = compromisso.Contato.Empresa,
            //        Cargo = compromisso.Contato.Cargo
            //    }
            //};

            return compromissoView;
        }

        [HttpPost]
        public string Inserir(InserirCompromissoViewModel compromissoViewModel)
        {
            //var contato = servicoContato.SelecionarPorId(compromissoViewModel.Contato.Id).Value;

            //var compromisso = new Compromisso(compromissoViewModel.Assunto, compromissoViewModel.Local,
            //    compromissoViewModel.Link, compromissoViewModel.Data, compromissoViewModel.HoraInicio,
            //    compromissoViewModel.HoraTermino, contato);

            var compromisso = mapeador.Map<Compromisso>(compromissoViewModel);

            var result = servicoCompromisso.Inserir(compromisso);

            if (result.IsFailed)
            {
                string[] erros = result.Errors.Select(e => e.Message).ToArray();
                return string.Join("\r\n", erros);
            }

            return "Inserido com sucesso!";
        }

        [HttpPut("{id}")]
        public string Editar(Guid id, InserirCompromissoViewModel compromissoViewModel)
        {
            var compromisso = servicoCompromisso.SelecionarPorId(id).Value;

            //compromisso.Assunto = compromissoViewModel.Assunto;
            //compromisso.Data = compromissoViewModel.Data;
            //compromisso.HoraInicio = compromissoViewModel.HoraInicio;
            //compromisso.HoraTermino = compromissoViewModel.HoraTermino;
            //compromisso.Data = compromissoViewModel.Data;
            //compromisso.Contato = servicoContato.SelecionarPorId(compromissoViewModel.Contato.Id).Value;

            var compromissoEditado = mapeador.Map(compromissoViewModel, compromisso);

            var resultado = servicoCompromisso.Editar(compromissoEditado);

            if (resultado.IsSuccess)
                return "Compromisso editado com sucesso!";

            string[] erros = resultado.Errors.Select(x => x.Message).ToArray();

            return string.Join("\r\n ", erros);
        }


        [HttpDelete("{id}")]
        public string Excluir(Guid id)
        {
            var resultadoBusca = servicoCompromisso.SelecionarPorId(id);

            List<string> erros = new List<string>();

            if (resultadoBusca.IsFailed)
            {
                erros = resultadoBusca.Errors.Select(x => x.Message).ToList();

                return string.Join("\r\n ", erros);
            }

            var compromisso = resultadoBusca.Value;

            var resultado = servicoCompromisso.Excluir(compromisso);

            if (resultado.IsSuccess)
                return "Compromisso excluido com sucesso!";

            erros = resultadoBusca.Errors.Select(x => x.Message).ToList();

            return string.Join("\r\n ", erros);
        }
    }
}

