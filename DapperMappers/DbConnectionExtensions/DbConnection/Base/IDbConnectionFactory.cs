using System.Data;

namespace DbConnectionExtensions.DbConnection.Base
{
    public interface IDbConnectionFactory
    {
        string ConnectionName { get; }
        
        IDbConnection Connection();
    }
}
