using Dapper;
using DapperMappers.Core.DbConnection;
using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.Diagnostics;
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
            if (File.Exists(_fileName))
            {
                File.Delete(_fileName);
            }
        }

        public IDbConnection Connection()
        {
            var connectionString = $"Filename={_fileName}";
            var conn = new SqliteConnection(connectionString);

            return conn;
        }

        public IDbConnection Connection(string name)
        {
            return Connection();
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
                conn.ExecuteAsync(@"create table Test_Objects
                   (
                      ID                                  integer primary key AUTOINCREMENT,
                      FirstName                           varchar(100) not null,
                      LastName                            varchar(100) not null,
                      StartWork                           datetime not null,
                      Content                             TEXT
                   )");
            }
        }
    }
}
