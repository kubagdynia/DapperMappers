using System.Data;
using Dapper;
using DbConnectionExtensions.DbConnection;
using Microsoft.Extensions.Configuration;

namespace DapperMappers.Domain.Tests.DbConnection
{
    public class BookDbConnectionFactory : SqliteDbConnectionFactory
    {
        public BookDbConnectionFactory(IConfiguration config, string connectionName) : base(config, connectionName)
        {
            
        }

        protected override void CreateDb(IDbConnection dbConnection)
        {
            dbConnection.Execute(
                @"CREATE TABLE Books
                (
                    InternalId                          integer primary key AUTOINCREMENT,
                    Id                                  varchar(36) not null,
                    Title                               varchar(200) not null,
                    PageCount                           integer null,
                    Isbn                                varchar(15) null,
                    DateOfPublication                   datetime not null,
                    Authors                             TEXT null,
                    TableOfContents                     TEXT null,
                    ShortDescription                    varchar(500) null,
                    Description                         TEXT null,
                    Publisher                           varchar(200) null,
                    Url                                 varchar(200) null
                )");
        }
    }
}
