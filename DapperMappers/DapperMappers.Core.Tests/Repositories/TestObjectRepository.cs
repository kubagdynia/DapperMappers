using Dapper;
using DapperMappers.Core.DbConnection;
using DapperMappers.Core.Tests.Models;
using System.Data;
using System.Linq;

namespace DapperMappers.Core.Tests.Repositories
{
    public interface ITestObjectRepository
    {
        TestXmlObject GetTestObject(long id);
        void SaveTestObject(TestXmlObject testObject);
        TestJsonObject GetTestJsonObject(long id);
        void SaveTestJsonObject(TestJsonObject testObject);
    }

    public class TestObjectRepository : ITestObjectRepository
    {
        private IDbConnection _connection;

        public TestObjectRepository(IDbConnectionFactory connectionFactory)
        {
            _connection = connectionFactory.Connection();
        }

        public TestXmlObject GetTestObject(long id)
        {
            TestXmlObject result = _connection.Query<TestXmlObject>(
                @"SELECT Id, FirstName, LastName, StartWork, Content
                    FROM Test_Objects
                    WHERE Id = @id", new { id }).FirstOrDefault();
            return result;
        }

        public void SaveTestObject(TestXmlObject testObject)
        {
            testObject.Id = _connection.Query<long>(
                @"INSERT INTO Test_Objects 
                    ( FirstName, LastName, StartWork, Content) VALUES 
                    ( @FirstName, @LastName, @StartWork, @Content );
                    select last_insert_rowid()", testObject).First();
        }

        public TestJsonObject GetTestJsonObject(long id)
        {
            
            TestJsonObject result = _connection.Query<TestJsonObject>(
                @"SELECT Id, FirstName, LastName, StartWork, Content
                    FROM Test_Objects
                    WHERE Id = @id", new { id }).FirstOrDefault();
            return result;
        }

        public void SaveTestJsonObject(TestJsonObject testObject)
        {
            testObject.Id = _connection.Query<long>(
                @"INSERT INTO Test_Objects 
                    ( FirstName, LastName, StartWork, Content) VALUES 
                    ( @FirstName, @LastName, @StartWork, @Content );
                    select last_insert_rowid()", testObject).First();
        }
    }
}
