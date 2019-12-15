using Dapper;
using DapperMappers.Core.Tests.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

namespace DapperMappers.Core.Tests.Repositories
{
    public interface ITestObjectRepository
    {
        void ConnectToDb(ISQLiteDbManagement dbManagement);
        TestObject GetTestObject(long id);
        void SaveTestObject(TestObject testObject);
    }

    public class TestObjectRepository : ITestObjectRepository
    {
        private ISQLiteDbManagement _dbManagement;

        public void ConnectToDb(ISQLiteDbManagement dbManagement)
        {
            _dbManagement = dbManagement;
        }

        public TestObject GetTestObject(long id)
        {
            using (var conn = _dbManagement.SimpleDbConnection())
            {
                conn.Open();
                TestObject result = conn.Query<TestObject>(
                    @"SELECT Id, FirstName, LastName, StartWork, Content
                    FROM Test_Objects
                    WHERE Id = @id", new { id }).FirstOrDefault();
                return result;
            }
        }

        public void SaveTestObject(TestObject testObject)
        {
            using (var cnn = _dbManagement.SimpleDbConnection())
            {
                cnn.Open();
                testObject.Id = cnn.Query<long>(
                    @"INSERT INTO Test_Objects 
                    ( FirstName, LastName, StartWork, Content) VALUES 
                    ( @FirstName, @LastName, @StartWork, @Content );
                    select last_insert_rowid()", testObject).First();
            }
        }
    }

    public interface ISQLiteDbManagement
    {
        SQLiteConnection SimpleDbConnection();
        void DeleteDb();
        string CreateDb(Action<SQLiteConnection> execute);
    }

    public class SQLiteDbManagement : ISQLiteDbManagement
    {
        private string _dbFilename;

        public SQLiteConnection SimpleDbConnection()
        {
            return new SQLiteConnection($"Data Source={_dbFilename}");
        }

        public string CreateDb(Action<SQLiteConnection> execute)
        {
            DeleteDb();
            CreateDbFileName();

            using (SQLiteConnection conn = SimpleDbConnection())
            {
                conn.Open();
                execute(conn);
            }

            return _dbFilename;
        }

        public void DeleteDb()
        {
            if (File.Exists(_dbFilename))
            {
                File.Delete(_dbFilename);
            }
        }

        private void CreateDbFileName()
        {
            _dbFilename = Path.Combine(Environment.CurrentDirectory, $"TestDb_{Guid.NewGuid()}.sqlite");
        }
    }
}
