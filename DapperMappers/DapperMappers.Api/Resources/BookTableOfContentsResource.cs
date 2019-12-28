using System.Collections.Generic;

namespace DapperMappers.Api.Resources
{
    public class BookTableOfContentsResource
    {
        public List<ChapterResource> Chapters { get; set; }
    }

    public class ChapterResource
    {
        public string Number { get; set; }

        public string Name { get; set; }

        public List<SubsectionResource> Subsections { get; set; }
    }

    public class SubsectionResource
    {
        public string Number { get; set; }

        public string Name { get; set; }
    }
}
