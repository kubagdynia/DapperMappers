using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DbConnectionExtensions.DbConnection
{
    public class DbConnectionFactory : BaseDbConnectionFactory
    {
        private readonly IConfiguration _config;
        
        public override string ConnectionName { get; }

        public DbConnectionFactory(IConfiguration config, string connectionName = "DefaultConnection")
        {
            _config = config;
            ConnectionName = connectionName;
        }

        public override IDbConnection Connection() => new SqlConnection(_config.GetConnectionString(ConnectionName));

        protected override void DisposeManageResource() { }

        protected override void DisposeUnManageResource() { }
    }
}
