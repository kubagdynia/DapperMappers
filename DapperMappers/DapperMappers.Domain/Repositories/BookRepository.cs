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

        public async Task<Book> GetBook(long internalId)
        {
            using (var conn = _connectionFactory.Connection())
            {
                Book book = await conn.QueryFirstAsync<Book>(_commandQuery.GetBookByInternalId, new { internalId });
                return book;
            }
        }

        public async Task<Book> GetBook(string id)
        {
            using (var conn = _connectionFactory.Connection())
            {
                Book book = await conn.QueryFirstAsync<Book>(_commandQuery.GetBookById, new { id });
                return book;
            }
        }

        public async Task SaveBook(Book book)
        {
            using (var conn = _connectionFactory.Connection())
            {
                book.InternalId = await conn.QueryFirstAsync<long>(_commandQuery.SaveBook, book);
            }
        }
    }
}