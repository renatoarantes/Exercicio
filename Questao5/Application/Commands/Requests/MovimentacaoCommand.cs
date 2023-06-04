using MediatR;

namespace Questao5.Application.Commands.Requests
{
    /// <summary>
    /// Request do parâmetro da Movimentação de Conta Bancária
    /// </summary>
    public class MovimentacaoCommand : IRequest<string>
    {
        /// <summary>
        /// Id da Requisição da movimentação bancária
        /// </summary>
        public Guid IdRequisicao { get; set; }

        /// <summary>
        /// Numero da conta corrente da conta bancária
        /// </summary>
        public Guid IdContaCorrente { get; set; }

        /// <summary>
        /// Valor a ser debitado ou creditado
        /// </summary>
        public double Valor { get; set; }

        /// <summary>
        /// Tipo de movimentação: C para crédito / D para débito
        /// </summary>
        public string TipoMovimento { get; set; }
    }
}
