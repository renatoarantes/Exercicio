using Questao5.Application.Queries.Responses;
using Questao5.Domain;
using Questao5.Domain.Entities;
using Questao5.Domain.Shared;
using Questao5.Infrastructure.Database.QueryStore;
using static Questao5.Domain.Enumerators.Enumerators;

namespace Questao5.Application.Queries
{
    public class SaldoQuery : IQuery<string, SaldoResponse>
    {
        private readonly IQueryStore<double, string> _saldoQueryStore;
        private readonly IQueryStore<ContaCorrente, string> _contaCorrenteQueryStore;

        public SaldoQuery(IQueryStore<double, string> saldoQueryStore,
                          IQueryStore<ContaCorrente, string> contaCorrenteQueryStore)
        {
            this._saldoQueryStore = saldoQueryStore;
            this._contaCorrenteQueryStore = contaCorrenteQueryStore;
        }
        public async Task<SaldoResponse> Get(string id)
        {
            var contaCorrente = await _contaCorrenteQueryStore.Get(id.ToUpper());

            Utils.VerifyContaCorrente(contaCorrente);

            var saldo = await _saldoQueryStore.Get(id.ToString().ToUpper());

            return new SaldoResponse
            {
                DataHoraConsulta = DateTime.Now,
                Saldo = saldo,
                Nome = contaCorrente.Nome,
                Numero = contaCorrente.Numero
            };
        }
    }
}
