using System.Collections.Generic;

namespace DapperMappers.Api.Contracts.V1.Resources
{
    public record BookTableOfContentsResource
    {
        public List<ChapterResource> Chapters { get; set; }
    }

    public record ChapterResource
    {
        public string Number { get; set; }

        public string Name { get; set; }

        public List<SubsectionResource>? Subsections { get; set; }
    }

    public record SubsectionResource
    {
        public string Number { get; set; }

        public string Name { get; set; }
    }
}
