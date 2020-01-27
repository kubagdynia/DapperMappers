namespace DbConnectionExtensions.DbConnection
{
    public class SQLiteConfiguration
    {
        public string DbFilename { get; set; }
        
        public bool DeleteDbFileOnExit { get; set; }
    }
}