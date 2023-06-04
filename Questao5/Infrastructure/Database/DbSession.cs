using static Dapper.SqlMapper;
using System.Data;
using Microsoft.Data.Sqlite;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database
{
    public class DbSession : IDisposable
    {
        private Guid _id;
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        private readonly DatabaseConfig _databaseConfig;

        public DbSession(DatabaseConfig databaseConfig)
        {
            this._databaseConfig = databaseConfig;
            _id = Guid.NewGuid();
            Connection = new SqliteConnection(_databaseConfig.Name);
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}
