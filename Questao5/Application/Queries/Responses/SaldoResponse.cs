namespace Questao5.Application.Queries.Responses
{
    /// <summary>
    /// retorno da consulta de saldo da conta corrente
    /// </summary>
    public class SaldoResponse
    {
        /// <summary>
        /// Número da conta corrente
        /// </summary>
        public Int64 Numero { get; set; }

        /// <summary>
        /// Nome do titular da conta corrente
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Data/Hora da consulta do saldo
        /// </summary>
        public DateTime DataHoraConsulta { get; set; }

        /// <summary>
        /// Valor correspondente ao saldo da conta corrente consultada
        /// </summary>
        public double Saldo { get; set; }
    }
}
