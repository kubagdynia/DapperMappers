using System;
using System.Data;

namespace DbConnectionExtensions.DbConnection
{
    public abstract class BaseDbConnectionFactory : IDbConnectionFactory
    {
        public abstract string ConnectionName { get; }
        
        public abstract IDbConnection Connection();
    }
}