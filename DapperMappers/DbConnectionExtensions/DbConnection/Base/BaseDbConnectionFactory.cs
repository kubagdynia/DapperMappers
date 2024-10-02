using System.Data;

namespace DbConnectionExtensions.DbConnection.Base
{
    public abstract class BaseDbConnectionFactory : IDbConnectionFactory
    {
        public abstract string ConnectionName { get; }
        
        public abstract IDbConnection Connection();
    }
}