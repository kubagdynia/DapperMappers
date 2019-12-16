using System;
using System.Data.SQLite;
using System.IO;

namespace DapperMappers.Core.Tests.Repositories
{
    public interface ISqLiteDbManagement
    {
        SQLiteConnection SimpleDbConnection();
        void DeleteDb();
        string CreateDb(Action<SQLiteConnection> execute);
    }
    
    public class SqLiteDbManagement : ISqLiteDbManagement
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