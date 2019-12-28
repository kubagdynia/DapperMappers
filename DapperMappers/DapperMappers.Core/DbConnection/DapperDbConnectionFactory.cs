using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;

namespace DapperMappers.Core.DbConnection
{
    public class DapperDbConnectionFactory : IDbConnectionFactory
    {
        private const string DefaultConnectionString = "DefaultConnection";

        private readonly IConfiguration _config;

        public DapperDbConnectionFactory(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection()
        {
            return Connection(DefaultConnectionString);
        }

        public IDbConnection Connection(string name)
        {
            string connectionString = _config.GetConnectionString(name);
            return new SqlConnection(connectionString);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                
            }
        }
    }
}
