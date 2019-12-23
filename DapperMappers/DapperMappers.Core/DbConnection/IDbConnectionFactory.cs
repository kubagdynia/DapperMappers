using System;
using System.Data;

namespace DapperMappers.Core.DbConnection
{
    public interface IDbConnectionFactory : IDisposable
    {
        IDbConnection Connection();

        IDbConnection Connection(string name);

        void CleanUp();
    }
}
