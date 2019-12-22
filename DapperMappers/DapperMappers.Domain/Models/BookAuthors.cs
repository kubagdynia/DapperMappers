using System.Collections.Generic;
using DapperMappers.Core.TypeHandlers;

namespace DapperMappers.Domain.Models
{
    public class BookAuthors : IXmlObjectType
    {
        public List<Author> Authors  { get; set; }
    }

    public class Author
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}