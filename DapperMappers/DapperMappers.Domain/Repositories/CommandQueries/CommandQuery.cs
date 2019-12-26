namespace DapperMappers.Domain.Repositories
{
    public class CommandQuery : ICommandQuery
    {
        public string GetBook =>
            @"SELECT Id, Name, PageCount, Isbn, DateOfPublication, Authors, TableOfContents,
                ShortDescription, Description, Publisher, Url FROM Books WHERE Id = @id";

        public string SaveBook =>
            @"INSERT INTO Books (Name, PageCount, Isbn, DateOfPublication, Authors, TableOfContents,
                ShortDescription, Description, Publisher, Url)
              VALUES (@Name, @PageCount, @Isbn, @DateOfPublication, @Authors, @TableOfContents,
                @ShortDescription, @Description, @Publisher, @Url);
              SELECT last_insert_rowid()";
    }
}
