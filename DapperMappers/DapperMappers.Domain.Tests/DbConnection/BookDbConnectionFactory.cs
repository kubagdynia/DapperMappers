using System;
using System.Data;
using Dapper;
using DapperMappers.Core.Tests.DbConnection;

namespace DapperMappers.Domain.Tests.DbConnection
{
    public class BookDbConnectionFactory : BaseSqliteConnectionFactory
    {
        public BookDbConnectionFactory() : base($"BookDb_{Guid.NewGuid()}.sqlite")
        {
            
        }

        public override void CreateDb(IDbConnection dbConnection)
        {
            dbConnection.Execute(
                @"CREATE TABLE Books
                (
                    ID                                  integer primary key AUTOINCREMENT,
                    Name                                varchar(200) not null,
                    PageCount                           integer not null,
                    Isbn                                integer null,
                    DateOfPublication                   datetime not null,
                    Authors                             TEXT,
                    TableOfContents                     TEXT,
                    ShortDescription                    varchar(500) null,
                    Description                         TEXT,
                    Publisher                           varchar(100) null,
                    Url                                 varchar(150)
                )");
        }
    }
}
