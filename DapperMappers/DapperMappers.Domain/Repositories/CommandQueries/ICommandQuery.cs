namespace DapperMappers.Domain.Repositories
{
    public interface ICommandQuery
    {
        string GetBookByInternalId { get; }

        string GetBookById { get; }

        string SaveBook { get; }
    }
}
