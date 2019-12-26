using System;

namespace DapperMappers.Domain.Models
{
    public class Book
    {
        public long Id { get; set; }

        public string Name { get; set; }
        
        public int PageCount { get; set; }
        
        public long Isbn { get; set; }
        
        public DateTime DateOfPublication { get; set; }
        
        public BookAuthors Authors { get; set; }
        
        public BookTableOfContents TableOfContents { get; set; }
        
        public string ShortDescription { get; set; }
        
        public BookDescription Description { get; set; }

        public string Publisher { get; set; }
        
        public string Url { get; set; }
    }
}