using DapperMappers.Core.DbConnection;
using System;

namespace DapperMappers.Core.Tests.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly IDbConnectionFactory ConnectionFactory;

        protected BaseRepository(IDbConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory;
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
                ConnectionFactory.CleanUp();
            }
        }
    }
}
