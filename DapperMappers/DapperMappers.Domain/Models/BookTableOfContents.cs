using Dapper.CustomTypeHandlers.TypeHandlers;
using System.Collections.Generic;

namespace DapperMappers.Domain.Models;

public record BookTableOfContents : IXmlObjectType
{
    public List<Chapter> Chapters { get; set; }
}

public record Chapter
{
    public string Number { get; set; }
        
    public string Name { get; set; }
        
    public List<Subsection> Subsections { get; set; }
}

public record Subsection
{
    public string Number { get; set; }
        
    public string Name { get; set; }
}