namespace DapperMappers.Domain.Repositories
{
    public interface ICommandQuery
    {
        string GetBookByInternalId { get; }

        string GetBookById { get; }

        string GetAllBooks { get; }

        string SaveBook { get; }
    }
}
