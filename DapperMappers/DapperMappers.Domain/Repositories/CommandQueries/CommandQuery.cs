namespace DapperMappers.Domain.Repositories.CommandQueries;

public class CommandQuery : ICommandQuery
{
    public string GetBookByInternalId =>
        @"SELECT InternalId, Id, Title, PageCount, Isbn, DateOfPublication, Authors, TableOfContents,
                ShortDescription, Description, Publisher, Url FROM Books WHERE InternalId = @internalId";

    public string GetBookById =>
        @"SELECT InternalId, Id, Title, PageCount, Isbn, DateOfPublication, Authors, TableOfContents,
                ShortDescription, Description, Publisher, Url FROM Books WHERE Id = @id";

    public string GetAllBooks =>
        @"SELECT InternalId, Id, Title, PageCount, Isbn, DateOfPublication, Authors, TableOfContents,
                ShortDescription, Description, Publisher, Url FROM Books";

    public string SaveBook =>
        @"INSERT INTO Books (Id, Title, PageCount, Isbn, DateOfPublication, Authors,
                TableOfContents, ShortDescription, Description, Publisher, Url)
              VALUES (@Id, @Title, @PageCount, @Isbn, @DateOfPublication, @Authors, @TableOfContents,
                @ShortDescription, @Description, @Publisher, @Url)";

    public string DeleteBook =>
        @"DELETE FROM Books WHERE Id = @id";
}