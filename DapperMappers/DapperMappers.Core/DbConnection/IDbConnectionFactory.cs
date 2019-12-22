using System.Data;

namespace DapperMappers.Core.DbConnection
{
    public interface IDbConnectionFactory
    {
        IDbConnection Connection();

        IDbConnection Connection(string name);

        void CleanUp();
    }
}
