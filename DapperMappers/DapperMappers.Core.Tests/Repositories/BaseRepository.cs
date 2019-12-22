using DapperMappers.Core.DbConnection;
using System;

namespace DapperMappers.Core.Tests.Repositories
{
    public abstract class BaseRepository : IBaseRepository
    {
        protected IDbConnectionFactory _connectionFactory;

        public BaseRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
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
                _connectionFactory.CleanUp();
            }
        }
    }

    public interface IBaseRepository : IDisposable
    {

    }
}
