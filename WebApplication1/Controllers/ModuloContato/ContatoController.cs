using AutoMapper;
using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Infra.Orm;
using eAgenda.Infra.Orm.ModuloContato;
using FluentResults;
using Microsoft.AspNetCore.Http.Extensions;
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
        private readonly ILogger<ContatoController> logger;
        public ContatoController(ServicoContato servicoContato, IMapper mapeador, ILogger<ContatoController> logger)
        {
            this.servicoContato = servicoContato;
            this.mapeador = mapeador;
            this.logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListarContatoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult SelecioarTodos(StatusFavoritoEnum status)
        {
            logger.LogInformation("Selecionando todos os contatos!", status);

            var contatos = servicoContato.SelecionarTodos(status).Value;

            return Ok(new
            {
                Sucesso = true,
                Dados = mapeador.Map<List<ListarContatoViewModel>>(contatos),
                QtdRegistros = contatos.Count
            });
        }

        [HttpGet("visualizacao-completa/{id}")]
        [ProducesResponseType(typeof(VisualizarContatoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult SelecionarPorId(Guid id)
        {
            logger.LogInformation("Selecionando por Id", id);

            var contato = servicoContato.SelecionarPorId(id);

            if (contato.IsFailed)
                return NotFound(new
                {
                    Sucesso = false,
                    Erros = contato.Errors.Select(x => x.Message)
                });


            return Ok(new
            {
                Sucesso = true,
                Dados = mapeador.Map<VisualizarContatoViewModel>(contato)
            });
        }

        [HttpPost]
        [ProducesResponseType(typeof(InserirContatoViewModel), 201)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]

        public IActionResult Inserir(InserirContatoViewModel contatoViewModel)
        {
            var contato = mapeador.Map<Contato>(contatoViewModel);

            return ProcessarResultado(contato);

        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(InserirContatoViewModel), 201)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult Editar(Guid id, InserirContatoViewModel contatoViewModel)
        {
            
            var resultadoSelecao = servicoContato.SelecionarPorId(id);

            if (resultadoSelecao.IsFailed)
                return NotFound(new
                {
                    Sucesso = false,
                    Erros = resultadoSelecao.Errors.Select(e => e.Message)
                });

            var contato = mapeador.Map(contatoViewModel, resultadoSelecao.Value);

            return ProcessarResultado(servicoContato.Editar(contato), contatoViewModel);
        }

        [HttpDelete]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public IActionResult Excluir(Guid id)
        {
            
            var resultadoBusca = servicoContato.SelecionarPorId(id);

            if (resultadoBusca.IsFailed)
                return NotFound(new
                {
                    Sucesso = false,
                    Erros = resultadoBusca.Errors.Select(x => x.Message)
                });

            return ProcessarResultado(servicoContato.Excluir(resultadoBusca.Value));
        }

        private IActionResult ProcessarResultado(Result<Contato> contatoResult, InserirContatoViewModel contatoViewModel = null)
        {
            if (contatoResult.IsFailed)
                return BadRequest(new
                {
                    Sucesso = false,
                    Erros = contatoResult.Errors.Select(x => x.Message)
                });

            return Ok(new
            {
                Sucesso = true,
                Dados = contatoViewModel
            });
        }
    }
}
