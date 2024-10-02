using System;
using Dapper;
using DapperMappers.Domain.Models;
using DbConnectionExtensions.DbConnection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DapperMappers.Domain.Repositories.CommandQueries;
using DbConnectionExtensions.DbConnection.Base;

namespace DapperMappers.Domain.Repositories;

public class BookRepository : IBookRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly ICommandQuery _commandQuery;

    public BookRepository(IEnumerable<IDbConnectionFactory> connectionFactory, ICommandQuery commandQuery, string connectionName = "")
    {
        _connectionFactory = string.IsNullOrEmpty(connectionName)
            ? connectionFactory.FirstOrDefault()
            : connectionFactory.FirstOrDefault(c => c.ConnectionName == connectionName);
            
        if (_connectionFactory == null)
        {
            throw new ArgumentNullException(nameof(connectionFactory));
        }
            
        _commandQuery = commandQuery;
    }

    public async Task<Book> GetBook(long internalId)
    {
        using var conn = _connectionFactory.Connection();
        var book = await conn.QueryFirstOrDefaultAsync<Book>(_commandQuery.GetBookByInternalId, new { internalId });
        return book;
    }

    public async Task<Book> GetBook(string id)
    {
        using var conn = _connectionFactory.Connection();
        var book = await conn.QueryFirstOrDefaultAsync<Book>(_commandQuery.GetBookById, new { id });
        return book;
    }

    public async Task<IEnumerable<Book>> GetAllBooks()
    {
        using var conn = _connectionFactory.Connection();
        var books = await conn.QueryAsync<Book>(_commandQuery.GetAllBooks);
        return books;
    }

    public async Task SaveBook(Book book)
    {
        using var conn = _connectionFactory.Connection();
        //book.InternalId = await conn.QueryFirstAsync<long>(_commandQuery.SaveBook, book);
        await conn.ExecuteAsync(_commandQuery.SaveBook, book);
    }

    public async Task DeleteBook(string id)
    {
        using var conn = _connectionFactory.Connection();
        await conn.ExecuteAsync(_commandQuery.DeleteBook, new { id });
    }
}