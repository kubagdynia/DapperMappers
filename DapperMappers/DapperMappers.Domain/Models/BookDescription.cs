using Dapper.CustomTypeHandlers.TypeHandlers;
using System.Collections.Generic;

namespace DapperMappers.Domain.Models
{
    public class BookDescription : IXmlObjectType
    {
        public Learn Learn { get; set; }
        
        public string About { get; set; }
        
        public Features Features { get; set; }
    }

    public class Learn
    {
        public List<string> Points { get; set; }
    }

    public class Features
    {
        public List<string> Points { get; set; }
    }
}