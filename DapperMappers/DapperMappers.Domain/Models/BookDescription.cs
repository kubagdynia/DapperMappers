using Dapper.CustomTypeHandlers.TypeHandlers;
using System.Collections.Generic;

namespace DapperMappers.Domain.Models;

public record BookDescription : IXmlObjectType
{
    public Learn Learn { get; set; }
        
    public string About { get; set; }
        
    public Features Features { get; set; }
}

public record Learn
{
    public List<string> Points { get; set; }
}

public record Features
{
    public List<string> Points { get; set; }
}