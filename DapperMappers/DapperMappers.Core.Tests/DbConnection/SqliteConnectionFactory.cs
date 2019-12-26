using Dapper;
using System;
using System.Data;

namespace DapperMappers.Core.Tests.DbConnection
{
    public class SqliteConnectionFactory : BaseSqliteConnectionFactory
    {
        public SqliteConnectionFactory() : base($"TestDb_{Guid.NewGuid()}.sqlite")
        {

        }

        public override void CreateDb(IDbConnection dbConnection)
        {
            dbConnection.Execute(
                @"create table Test_Objects
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
