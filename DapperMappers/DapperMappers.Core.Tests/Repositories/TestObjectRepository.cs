using Dapper;
using DapperMappers.Core.DbConnection;
using DapperMappers.Core.Tests.Models;
using System;
using System.Linq;

namespace DapperMappers.Core.Tests.Repositories
{
    public interface ITestObjectRepository : IDisposable
    {
        TestXmlObject GetTestObject(long id);
        void SaveTestObject(TestXmlObject testObject);
        TestJsonObject GetTestJsonObject(long id);
        void SaveTestJsonObject(TestJsonObject testObject);
    }

    public class TestObjectRepository : BaseRepository, ITestObjectRepository
    {
        public TestObjectRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory) 
        {
            
        }

        public TestXmlObject GetTestObject(long id)
        {
            using (var conn = _connectionFactory.Connection())
            {
                conn.Open();
                try
                {
                    TestXmlObject result = conn.Query<TestXmlObject>(
                        @"SELECT Id, FirstName, LastName, StartWork, Content
                        FROM Test_Objects
                        WHERE Id = @id", new { id }).FirstOrDefault();
                    return result;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void SaveTestObject(TestXmlObject testObject)
        {
            using (var conn = _connectionFactory.Connection())
            {
                conn.Open();
                try
                {
                    testObject.Id = conn.Query<long>(
                        @"INSERT INTO Test_Objects 
                        ( FirstName, LastName, StartWork, Content) VALUES 
                        ( @FirstName, @LastName, @StartWork, @Content );
                        select last_insert_rowid()", testObject).First();
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public TestJsonObject GetTestJsonObject(long id)
        {
            using (var conn = _connectionFactory.Connection())
            {
                conn.Open();
                try
                {
                    TestJsonObject result = conn.Query<TestJsonObject>(
                        @"SELECT Id, FirstName, LastName, StartWork, Content
                        FROM Test_Objects
                        WHERE Id = @id", new { id }).FirstOrDefault();
                    return result;

                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void SaveTestJsonObject(TestJsonObject testObject)
        {
            using (var conn = _connectionFactory.Connection())
            {
                conn.Open();
                try
                {
                    testObject.Id = conn.Query<long>(
                        @"INSERT INTO Test_Objects 
                        ( FirstName, LastName, StartWork, Content) VALUES 
                        ( @FirstName, @LastName, @StartWork, @Content );
                        select last_insert_rowid()", testObject).First();
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
