using Dapper;
using Questao5.Domain.Entities;
using Questao5.Domain.Shared;

namespace Questao5.Infrastructure.Database.CommandStore
{
    public class MovimentoCommandStore : ICommandStore<Movimento>
    {
        private readonly DbSession _session;

        public MovimentoCommandStore(DbSession session)
        {
            _session = session;
        }
        public async Task Add(Movimento item)
        {
            try
            {
                var query = CommandStoreSQL.insertMovimentoSQL;
                await _session.Connection.ExecuteAsync(query, item);
            }
            catch (CustomException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
