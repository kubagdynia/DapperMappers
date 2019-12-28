using DapperMappers.Core.DbConnection;
using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.IO;

namespace DapperMappers.Core.Tests.DbConnection
{
    public abstract class BaseSqliteConnectionFactory : IDbConnectionFactory
    {
        private readonly string _fileName;
        private readonly bool _deleteDbOnExit;

        public BaseSqliteConnectionFactory(string dbFilename, bool deleteDbOnExit = true)
        {
            _fileName = Path.Combine(Environment.CurrentDirectory, dbFilename);
            _deleteDbOnExit = deleteDbOnExit;

            InitializeDatabase();            
        }

        public IDbConnection Connection()
        {
            var connectionString = $"DataSource={_fileName}";
            var conn = new SqliteConnection(connectionString);

            return conn;
        }

        public IDbConnection Connection(string name)
        {
            return Connection();
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
                CleanUp();
            }
        }

        public abstract void CreateDb(IDbConnection dbConnection);

        private void CleanUp()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

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
    }
}
