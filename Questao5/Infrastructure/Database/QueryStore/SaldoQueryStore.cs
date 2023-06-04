using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.QueryStore
{
    public class SaldoQueryStore : IQueryStore<double, string>
    {
        private readonly DatabaseConfig _databaseConfig;

        public SaldoQueryStore(DatabaseConfig databaseConfig)
        {
            this._databaseConfig = databaseConfig;
        }

        public async Task<double> Get(string id)
        {
            try
            {
                using (var connection = new SqliteConnection(_databaseConfig.Name))
                {
                    var query = QuerySQL.selectSaldoContaCorrente;

                    var result = await connection.ExecuteScalarAsync(query, new { IdContaCorrente = id });

                    return (double)result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
