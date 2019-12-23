using Dapper;
using DapperMappers.Core.DbConnection;
using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.IO;

namespace DapperMappers.Core.Tests.DbConnection
{
    public class SqliteConnectionFactory : IDbConnectionFactory
    {
        private readonly string _fileName;

        public SqliteConnectionFactory()
        {
            _fileName = Path.Combine(Environment.CurrentDirectory, $"TestDb_{Guid.NewGuid()}.sqlite");

            InitializeDatabase();
        }

        public void CleanUp()
        {

            GC.Collect();
            GC.WaitForPendingFinalizers();   

            if (File.Exists(_fileName))
            {
                File.Delete(_fileName);
            }
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
                    conn.ExecuteAsync(@"create table Test_Objects
                       (
                          ID                                  integer primary key AUTOINCREMENT,
                          FirstName                           varchar(100) not null,
                          LastName                            varchar(100) not null,
                          StartWork                           datetime not null,
                          Content                             TEXT
                       )");
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
