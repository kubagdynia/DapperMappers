using System.Data;

namespace DbConnectionExtensions.DbConnection
{
    public interface IDbConnectionFactory
    {
        string ConnectionName { get; }
        
        IDbConnection Connection();
    }
}
