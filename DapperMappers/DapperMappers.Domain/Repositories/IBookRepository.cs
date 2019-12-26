using DapperMappers.Domain.Models;
using System.Threading.Tasks;

namespace DapperMappers.Domain.Repositories
{
    public interface IBookRepository
    {
        Task<Book> GetBook(long id);
        Task SaveBook(Book book);
    }
}