using System.Collections.Generic;
using DapperMappers.Core.TypeHandlers;

namespace DapperMappers.Domain.Models
{
    public class BookTableOfContents : IXmlObjectType
    {
        public List<Chapter> Chapters { get; set; }
    }

    public class Chapter
    {
        public string Number { get; set; }
        
        public string Name { get; set; }
        
        public List<Subsection> Subsections { get; set; }
    }

    public class Subsection
    {
        public string Number { get; set; }
        
        public string Name { get; set; }
    }
}