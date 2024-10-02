using DapperMappers.Api.Contracts.V1.Resources;
using System;

namespace DapperMappers.Api.Contracts.V1.Requests;

public record AddBookRequest
{
    public string Title { get; set; } = string.Empty;

    public int PageCount { get; set; }

    public string Isbn { get; set; } = string.Empty;

    public DateTime DateOfPublication { get; set; }

    public BookAuthorsResource Authors { get; set; } = new();

    public BookTableOfContentsResource TableOfContents { get; set; } = new();

    public string ShortDescription { get; set; } = string.Empty;

    public BookDescriptionResource Description { get; set; } = new();

    public string Publisher { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;
}