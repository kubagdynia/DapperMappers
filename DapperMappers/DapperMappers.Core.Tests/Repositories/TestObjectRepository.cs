using Dapper;
using DapperMappers.Core.DbConnection;
using DapperMappers.Core.Tests.Models;
using System.Threading.Tasks;

namespace DapperMappers.Core.Tests.Repositories
{
    public interface ITestObjectRepository
    {
        Task<TestXmlObject> GetTestObject(long id);
        Task SaveTestObject(TestXmlObject testObject);
        Task<TestJsonObject> GetTestJsonObject(long id);
        Task SaveTestJsonObject(TestJsonObject testObject);
    }

    public class TestObjectRepository : ITestObjectRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public TestObjectRepository(IDbConnectionFactory connectionFactory) => _connectionFactory = connectionFactory;

        public async Task<TestXmlObject> GetTestObject(long id)
        {
            using (var conn = _connectionFactory.Connection())
            {
                TestXmlObject result = await conn.QueryFirstAsync<TestXmlObject>(
                    @"SELECT Id, FirstName, LastName, StartWork, Content FROM Test_Objects WHERE Id = @id", new { id });
                
                return result;
            }
        }

        public async Task SaveTestObject(TestXmlObject testObject)
        {
            using (var conn = _connectionFactory.Connection())
            {
                testObject.Id = await conn.QueryFirstAsync<long>(
                    @"INSERT INTO Test_Objects (FirstName, LastName, StartWork, Content)
                         VALUES (@FirstName, @LastName, @StartWork, @Content);
                      select last_insert_rowid()", testObject);
            }
        }

        public async Task<TestJsonObject> GetTestJsonObject(long id)
        {
            using (var conn = _connectionFactory.Connection())
            {
                var result = await conn.QueryFirstAsync<TestJsonObject>(
                    @"SELECT Id, FirstName, LastName, StartWork, Content
                      FROM Test_Objects
                      WHERE Id = @id", new {id});

                return result;
            }
        }

        public async Task SaveTestJsonObject(TestJsonObject testObject)
        {
            using (var conn = _connectionFactory.Connection())
            {
                testObject.Id = await conn.QueryFirstAsync<long>(
                    @"INSERT INTO Test_Objects (FirstName, LastName, StartWork, Content)
                         VALUES (@FirstName, @LastName, @StartWork, @Content);
                      select last_insert_rowid()", testObject);                
            }
        }
    }
}
