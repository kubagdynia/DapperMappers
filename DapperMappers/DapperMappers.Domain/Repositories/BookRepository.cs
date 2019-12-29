using Dapper;
using DapperMappers.Core.DbConnection;
using DapperMappers.Domain.Models;
using System.Collections.Generic;
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
                Book book = await conn.QueryFirstOrDefaultAsync<Book>(_commandQuery.GetBookByInternalId, new { internalId });
                return book;
            }
        }

        public async Task<Book> GetBook(string id)
        {
            using (var conn = _connectionFactory.Connection())
            {
                Book book = await conn.QueryFirstOrDefaultAsync<Book>(_commandQuery.GetBookById, new { id });
                return book;
            }
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            using (var conn = _connectionFactory.Connection())
            {
                IEnumerable<Book> books = await conn.QueryAsync<Book>(_commandQuery.GetAllBooks);
                return books;
            }
        }

        public async Task SaveBook(Book book)
        {
            using (var conn = _connectionFactory.Connection())
            {
                //book.InternalId = await conn.QueryFirstAsync<long>(_commandQuery.SaveBook, book);
                await conn.ExecuteAsync(_commandQuery.SaveBook, book);
            }
        }

        public async Task DeleteBook(string id)
        {
            using (var conn = _connectionFactory.Connection())
            {
                await conn.ExecuteAsync(_commandQuery.DeleteBook, new { id });
            }
        }
    }
}