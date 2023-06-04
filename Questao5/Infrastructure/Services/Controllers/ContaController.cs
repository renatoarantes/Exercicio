using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Queries;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Shared;
using Questao5.Infrastructure.Database.QueryStore;
using System.Windows.Input;
using static Questao5.Domain.Enumerators.Enumerators;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IQuery<string, SaldoResponse> _saldoQuery;

        public ContaController(IMediator mediator,
                               IQuery<string, SaldoResponse> saldoQuery)
        {
            this._mediator = mediator;
            this._saldoQuery = saldoQuery;
        }

        /// <summary>
        /// Consulta Saldo da conta corrente
        /// </summary>
        /// <param name="id">Passa a identificação da conta corrente</param>
        /// <returns>Retorna o saldo atual da conta corrente</returns>
        /// <response code="200">Retorna o saldo atual da conta corrente</response>
        /// <response code="400">Retorna erro de parâmetro errado</response>   
        /// <response code="500">Ocorreu algum erro no servidor</response>   
        [HttpGet("Saldo/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesErrorResponseType(typeof(ENUM_CUSTOM_ERROR))]
        public async Task<ActionResult<SaldoResponse>> Get(Guid id)
        {
            try
            {
                return Ok(await _saldoQuery.Get(id.ToString()));
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); ;
            }
        }

        /// <summary>
        /// Adiciona uma nova movimentação na Conta Corrente
        /// </summary>
        /// <param name="command">Parametros da Movimentação da Conta Bancária</param>
        /// <returns>Retorna o Id da movimentação da conta corrente</returns>
        /// <response code="200">Retorna o Id da movimentação da conta corrente</response>
        /// <response code="400">Retorna erro de parâmetro errado</response>   
        /// <response code="500">Ocorreu algum erro no servidor</response>   
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesErrorResponseType(typeof(ENUM_CUSTOM_ERROR))]
        public async Task<ActionResult<string>> Post(MovimentacaoCommand command)
        {
            try
            {
                return Ok(await _mediator.Send(command));
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); ;
            }
        }
    }
}
