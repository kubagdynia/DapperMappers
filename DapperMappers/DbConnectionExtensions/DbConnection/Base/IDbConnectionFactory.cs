using System;
using System.Data;

namespace DbConnectionExtensions.DbConnection
{
    public interface IDbConnectionFactory : IDisposable
    {
        string ConnectionName { get; }
        
        IDbConnection Connection();
    }
}
