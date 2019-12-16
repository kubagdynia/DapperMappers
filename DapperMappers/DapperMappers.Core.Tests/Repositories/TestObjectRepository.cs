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
        void ConnectToDb(ISqLiteDbManagement dbManagement);
        TestXmlObject GetTestObject(long id);
        void SaveTestObject(TestXmlObject testObject);
        TestJsonObject GetTestObject2(long id);
        void SaveTestObject2(TestJsonObject testObject);
    }

    public class TestObjectRepository : ITestObjectRepository
    {
        private ISqLiteDbManagement _dbManagement;

        public void ConnectToDb(ISqLiteDbManagement dbManagement)
        {
            _dbManagement = dbManagement;
        }

        public TestXmlObject GetTestObject(long id)
        {
            using (var conn = _dbManagement.SimpleDbConnection())
            {
                conn.Open();
                TestXmlObject result = conn.Query<TestXmlObject>(
                    @"SELECT Id, FirstName, LastName, StartWork, Content
                    FROM Test_Objects
                    WHERE Id = @id", new { id }).FirstOrDefault();
                return result;
            }
        }

        public void SaveTestObject(TestXmlObject testObject)
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
        
        public TestJsonObject GetTestObject2(long id)
        {
            using (var conn = _dbManagement.SimpleDbConnection())
            {
                conn.Open();
                TestJsonObject result = conn.Query<TestJsonObject>(
                    @"SELECT Id, FirstName, LastName, StartWork, Content
                    FROM Test_Objects
                    WHERE Id = @id", new { id }).FirstOrDefault();
                return result;
            }
        }

        public void SaveTestObject2(TestJsonObject testObject)
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
}
