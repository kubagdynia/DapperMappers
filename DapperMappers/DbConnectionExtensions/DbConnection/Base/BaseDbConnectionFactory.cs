using System;
using System.Data;

namespace DbConnectionExtensions.DbConnection
{
    public abstract class BaseDbConnectionFactory : IDbConnectionFactory
    {
        private bool _disposed;

        public abstract string ConnectionName { get; }
        
        public abstract IDbConnection Connection();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed resources.
                DisposeManageResource();
            }
            
            // Dispose unmanaged resources.
            DisposeUnManageResource();

            _disposed = true;
        }

        protected abstract void DisposeManageResource();

        protected abstract void DisposeUnManageResource();

        ~BaseDbConnectionFactory() => Dispose(false);
    }
}