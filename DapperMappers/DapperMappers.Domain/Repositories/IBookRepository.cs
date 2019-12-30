using DapperMappers.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DapperMappers.Domain.Repositories
{
    public interface IBookRepository
    {
        Task<Book> GetBook(long internalId);
        Task<Book> GetBook(string id);
        Task<IEnumerable<Book>> GetAllBooks();
        Task SaveBook(Book book);
        Task DeleteBook(string id);
    }
}