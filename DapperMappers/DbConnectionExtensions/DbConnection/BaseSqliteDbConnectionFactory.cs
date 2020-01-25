using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.IO;

namespace DbConnectionExtensions.DbConnection
{
    public abstract class BaseSqliteDbConnectionFactory : IDbConnectionFactory
    {
        bool _disposed;

        private readonly string _fileName;
        private readonly bool _deleteDbOnExit;

        public BaseSqliteDbConnectionFactory(string dbFilename, bool deleteDbOnExit = true)
        {
            _fileName = Path.Combine(Environment.CurrentDirectory, dbFilename);
            _deleteDbOnExit = deleteDbOnExit;

            InitializeDatabase();
        }

        public IDbConnection Connection() => new SqliteConnection($"DataSource={_fileName}");

        public IDbConnection Connection(string name) => Connection();

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
                CleanUp();
            }

            _disposed = true;
        }

        public abstract void CreateDb(IDbConnection dbConnection);

        private void CleanUp()
        {
            //GC.Collect();
            //GC.WaitForPendingFinalizers();

            if (_deleteDbOnExit && File.Exists(_fileName))
            {
                File.Delete(_fileName);
            }
        }

        private void InitializeDatabase()
        {
            if (File.Exists(_fileName))
            {
                return;
            }

            FileStream fileStream = File.Create(_fileName);
            fileStream.Close();

            using (var conn = Connection())
            {
                conn.Open();
                try
                {
                    CreateDb(conn);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        ~BaseSqliteDbConnectionFactory() => Dispose(false);
    }
}
