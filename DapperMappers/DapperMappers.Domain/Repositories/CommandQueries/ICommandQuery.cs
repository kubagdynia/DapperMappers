namespace DapperMappers.Domain.Repositories
{
    public interface ICommandQuery
    {
        string GetBook { get; }

        string SaveBook { get; }
    }
}
