namespace DapperMappers.Domain.Repositories.CommandQueries;

public interface ICommandQuery
{
    string GetBookByInternalId { get; }

    string GetBookById { get; }

    string GetAllBooks { get; }

    string SaveBook { get; }

    string DeleteBook { get; }
}