using Dapper;
using DapperMappers.Core.DbConnection;
using DapperMappers.Domain.Models;
using System.Threading.Tasks;

namespace DapperMappers.Domain.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly ICommandQuery _commandQuery;

        public BookRepository(IDbConnectionFactory connectionFactory, ICommandQuery commandQuery)
        {
            _connectionFactory = connectionFactory;
            _commandQuery = commandQuery;
        }

        public async Task<Book> GetBook(long id)
        {
            using (var conn = _connectionFactory.Connection("BookDb"))
            {
                Book book = await conn.QueryFirstAsync<Book>(_commandQuery.GetBook, new { id });
                return book;
            }
        }

        public async Task SaveBook(Book book)
        {
            using (var conn = _connectionFactory.Connection("BookDb"))
            {
                book.Id = await conn.QueryFirstAsync<long>(_commandQuery.SaveBook, book);
            }
        }
    }
}