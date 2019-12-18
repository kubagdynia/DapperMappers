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
        private readonly IDbConnection _connection = null;

        private readonly string _fileName;

        public SqliteConnectionFactory()
        {
            _fileName = Path.Combine(Environment.CurrentDirectory, $"TestDb_{Guid.NewGuid()}.sqlite");

            var connectionString = $"FileName={_fileName}";
            _connection = new SqliteConnection(connectionString);

            CreateDb();
        }

        ~SqliteConnectionFactory()
        {
            if (File.Exists(_fileName))
            {
                File.Delete(_fileName);
            }
        }

        public IDbConnection Connection()
        {
            return _connection;
        }

        public IDbConnection Connection(string name)
        {
            return Connection();
        }
        
        private void CreateDb()
        {
            if (File.Exists(_fileName))
            {
                return;
            }
            
            FileStream fileStream = File.Create(_fileName);
            fileStream.Close();

            try
            {
                _connection.Open();

                _connection.Execute(@"create table Test_Objects
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
                _connection.Close();
            }
        }
    }
}
