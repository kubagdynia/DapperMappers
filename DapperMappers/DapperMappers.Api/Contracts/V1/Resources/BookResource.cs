using System;

namespace DapperMappers.Api.Contracts.V1.Resources
{
    public record BookResource
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public int PageCount { get; set; }

        public string Isbn { get; set; }

        public DateTime DateOfPublication { get; set; }

        public BookAuthorsResource Authors { get; set; }

        public BookTableOfContentsResource TableOfContents { get; set; }

        public string ShortDescription { get; set; }

        public BookDescriptionResource Description { get; set; }

        public string Publisher { get; set; }

        public string Url { get; set; }
    }
}
