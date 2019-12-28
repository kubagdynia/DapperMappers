using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DapperMappers.Api.Contracts;
using DapperMappers.Api.Contracts.V1.Requests;
using DapperMappers.Api.Contracts.V1.Responses;
using DapperMappers.Api.Resources;
using DapperMappers.Domain.Models;
using DapperMappers.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperMappers.Api.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BooksController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns a list of all books
        /// </summary>
        /// <response code="200">Success - Returns a list of all books</response>
        /// <response code="204">No Content - The are no books</response>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(type: typeof(GetAllBooksResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(type: typeof(EmptyResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<GetAllBooksResponse>> GetAllBooks()
        {
            var books = await _bookRepository.GetAllBooks();

            if (books is null || !books.Any())
            {
                return NoContent();
            }

            var bookResources = _mapper.Map<IEnumerable<Book>, IEnumerable<BookResource>>(books);

            var response = new GetAllBooksResponse(bookResources, StatusCodes.Status200OK);

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(type: typeof(EmptyResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(type: typeof(NotFoundMessage), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetBookResponse>> GetBook([FromRoute] GetBookRequest request)
        {
            var book = await _bookRepository.GetBook(request.Id.ToString());

            if (book is null)
            {
                return NotFound(new NotFoundMessage("Book not found"));
            }

            var bookResource = _mapper.Map<Book, BookResource>(book);

            var response = new GetBookResponse(bookResource, StatusCodes.Status200OK);

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(statusCode: StatusCodes.Status201Created)]
        public async Task<ActionResult<string>> AddBook(CreateBookResource createBookResource)
        {
            var book = _mapper.Map<CreateBookResource, Book>(createBookResource);

            book.Id = Guid.NewGuid().ToString();

            await _bookRepository.SaveBook(book);

            return CreatedAtAction(nameof(GetBook), new GetBookRequest { Id = Guid.Parse(book.Id) }, book.Id);
        }
    }
}