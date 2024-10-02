using Dapper.CustomTypeHandlers.TypeHandlers;
using System.Collections.Generic;

namespace DapperMappers.Domain.Models;

public record BookAuthors : IXmlObjectType
{
    public List<Author> Authors  { get; set; }
}

public record Author
{
    public string Name { get; set; }

    public string Description { get; set; }
}