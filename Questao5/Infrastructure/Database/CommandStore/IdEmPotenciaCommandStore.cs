using Dapper;
using Microsoft.Data.Sqlite;
using NSubstitute.Core;
using Questao5.Domain.Entities;
using Questao5.Domain.Shared;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.CommandStore
{
    public class IdEmPotenciaCommandStore : ICommandStore<IdEmPotencia>
    {
        private readonly DbSession _session;

        public IdEmPotenciaCommandStore(DbSession session)
        {
            _session = session;
        }
        public async Task Add(IdEmPotencia item)
        {
            try
            {
                var query = CommandStoreSQL.insertIdEmPotenciaSQL;
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
