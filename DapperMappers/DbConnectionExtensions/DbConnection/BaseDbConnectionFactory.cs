using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;

namespace DbConnectionExtensions.DbConnection
{
    public class BaseDbConnectionFactory : IDbConnectionFactory
    {
        bool _disposed;

        private const string DefaultConnectionString = "DefaultConnection";

        private readonly IConfiguration _config;

        public BaseDbConnectionFactory(IConfiguration config) => _config = config;

        public IDbConnection Connection() => Connection(DefaultConnectionString);

        public IDbConnection Connection(string name) => new SqlConnection(_config.GetConnectionString(name));

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {

            }

            _disposed = true;
        }

        ~BaseDbConnectionFactory() => Dispose(false);
    }
}
