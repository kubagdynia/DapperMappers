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
            using (var conn = ConnectionFactory.Connection())
            {
                TestXmlObject result = conn.Query<TestXmlObject>(
                    @"SELECT Id, FirstName, LastName, StartWork, Content
                    FROM Test_Objects
                    WHERE Id = @id", new { id }).FirstOrDefault();
                return result;
            }
        }

        public void SaveTestObject(TestXmlObject testObject)
        {
            using (var conn = ConnectionFactory.Connection())
            {
                testObject.Id = conn.Query<long>(
                    @"INSERT INTO Test_Objects 
                    ( FirstName, LastName, StartWork, Content) VALUES 
                    ( @FirstName, @LastName, @StartWork, @Content );
                    select last_insert_rowid()", testObject).First();
            }
        }

        public TestJsonObject GetTestJsonObject(long id)
        {
            using (var conn = ConnectionFactory.Connection())
            {
                TestJsonObject result = conn.Query<TestJsonObject>(
                    @"SELECT Id, FirstName, LastName, StartWork, Content
                    FROM Test_Objects
                    WHERE Id = @id", new { id }).FirstOrDefault();
                return result;
            }
        }

        public void SaveTestJsonObject(TestJsonObject testObject)
        {
            using (var conn = ConnectionFactory.Connection())
            {
                testObject.Id = conn.Query<long>(
                    @"INSERT INTO Test_Objects 
                    ( FirstName, LastName, StartWork, Content) VALUES 
                    ( @FirstName, @LastName, @StartWork, @Content );
                    select last_insert_rowid()", testObject).First();
            }
        }
    }
}
