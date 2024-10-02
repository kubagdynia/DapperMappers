using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.IO;
using DbConnectionExtensions.DbConnection.Base;
using Microsoft.Extensions.Configuration;

namespace DbConnectionExtensions.DbConnection
{
    public abstract class SqliteDbConnectionFactory : BaseDbConnectionFactory
    {
        private const string ConfigurationSection = "SQLiteConnectionStrings";

        private readonly string _fileName;
        private readonly bool _deleteDbOnExit;

        public override string ConnectionName { get; }

        protected SqliteDbConnectionFactory(IConfiguration config, string connectionName = "DefaultConnection")
        {
            ConnectionName = connectionName;

            SQLiteConfiguration sqliteConfiguration =
                config.GetSection($"{ConfigurationSection}:{connectionName}").Get<SQLiteConfiguration>();

            _fileName = Path.Combine(Environment.CurrentDirectory, sqliteConfiguration.DbFilename);
            _deleteDbOnExit = sqliteConfiguration.DeleteDbFileOnExit;

            InitializeDatabase();
        }

        public override IDbConnection Connection() => new SqliteConnection($"DataSource={_fileName}");

        protected abstract void CreateDb(IDbConnection dbConnection);

        private void InitializeDatabase()
        {
            if (File.Exists(_fileName))
            {
                return;
            }

            FileStream fileStream = File.Create(_fileName);
            fileStream.Close();

            using (var conn = Connection())
            {
                conn.Open();
                try
                {
                    CreateDb(conn);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
