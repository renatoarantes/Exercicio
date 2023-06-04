using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Shared;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.QueryStore
{
    public class ContaCorrenteQueryStore : IQueryStore<ContaCorrente, string>
    {
        private readonly DatabaseConfig _databaseConfig;

        public ContaCorrenteQueryStore(DatabaseConfig databaseConfig)
        {
            this._databaseConfig = databaseConfig;
        }

        public async Task<ContaCorrente> Get(string id)
        {
            try
            {
                using (var connection = new SqliteConnection(_databaseConfig.Name))
                {
                    var query = QuerySQL.selectContaCorrente;
                    return await connection.QueryFirstOrDefaultAsync<ContaCorrente>(query, new { IdContaCorrente = id });
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
