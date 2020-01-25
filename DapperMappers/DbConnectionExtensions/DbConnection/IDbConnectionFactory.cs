using System;
using System.Data;

namespace DbConnectionExtensions.DbConnection
{
    public interface IDbConnectionFactory : IDisposable
    {
        IDbConnection Connection();

        IDbConnection Connection(string name);
    }
}
