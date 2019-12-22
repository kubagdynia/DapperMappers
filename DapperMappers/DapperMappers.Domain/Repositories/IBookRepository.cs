using DapperMappers.Domain.Models;

namespace DapperMappers.Domain.Repositories
{
    public interface IBookRepository
    {
        Book GetBook(long id);
        void SaveBook(Book book);
    }
}