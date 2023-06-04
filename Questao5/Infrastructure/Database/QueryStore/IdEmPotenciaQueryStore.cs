using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Shared;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.QueryStore
{
    public class IdEmPotenciaQueryStore : IQueryStore<IdEmPotencia, string>
    {
        private readonly DatabaseConfig _databaseConfig;

        public IdEmPotenciaQueryStore(DatabaseConfig databaseConfig)
        {
            this._databaseConfig = databaseConfig;
        }
        public async Task<IdEmPotencia> Get(string id)
        {
            try
            {
                using (var connection = new SqliteConnection(_databaseConfig.Name))
                {
                    var query = QuerySQL.selectIdEmPotencia;
                    return await connection.QueryFirstOrDefaultAsync<IdEmPotencia>(query, new { chave_idempotencia = id });
                }
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
