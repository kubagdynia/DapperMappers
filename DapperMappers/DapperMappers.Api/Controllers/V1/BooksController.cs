using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DapperMappers.Api.Contracts.Core;
using DapperMappers.Api.Contracts.V1.Requests;
using DapperMappers.Api.Contracts.V1.Resources;
using DapperMappers.Api.Contracts.V1.Responses;
using DapperMappers.Api.Validators;
using DapperMappers.Domain.Models;
using DapperMappers.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperMappers.Api.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BooksController(IBookRepository bookRepository, IMapper mapper) : ControllerBase
    {
        /// <summary>
        /// Returns a list of all books
        /// </summary>
        /// <response code="200">Success - Returns a list of all books</response>
        /// <response code="204">No Content - The are no books</response>
        /// <returns>A list of all books</returns>
        [HttpGet]
        [ProducesResponseType(type: typeof(GetAllBooksResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<GetAllBooksResponse>> GetAllBooks()
        {
            var books = await bookRepository.GetAllBooks();

            if (books is null || !books.Any())
            {
                return NoContent();
            }

            var bookResources = mapper.Map<IEnumerable<Book>, IEnumerable<BookResource>>(books);

            var response = new GetAllBooksResponse(bookResources, StatusCodes.Status200OK);

            return Ok(response);
        }

        /// <summary>
        /// Returns the selected book
        /// </summary>
        /// <param name="request">Book id</param>
        /// <response code="200">Success - Returns the selected book</response>
        /// <response code="404">Not Found - Book not found</response>
        /// <returns>Selected book</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(type: typeof(NotFoundMessage), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetBookResponse>> GetBook([FromRoute] GetBookRequest request)
        {
            var book = await bookRepository.GetBook(request.Id.ToString());

            if (book is null)
            {
                return NotFound(new NotFoundMessage("Book not found"));
            }

            var bookResource = mapper.Map<Book, BookResource>(book);

            var response = new GetBookResponse(bookResource, StatusCodes.Status200OK);

            return Ok(response);
        }

        /// <summary>
        /// Add a book
        /// </summary>
        /// <param name="request">Book to be added</param>
        /// <response code="201">Success - The book has been added</response>
        /// <response code="400">Bad Request - The book cannot be added due to incorrect data</response>
        /// <returns>Book Id</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(type: typeof(BadRequestMessage), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AddBookResponse>> AddBook(AddBookRequest request)
        {
            var validator = new AddBookRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(new BadRequestMessage(validationResult.Errors));
            }

            var book = mapper.Map<AddBookRequest, Book>(request);

            book.Id = Guid.NewGuid().ToString();

            await bookRepository.SaveBook(book);

            return CreatedAtAction(nameof(GetBook), new GetBookRequest { Id = Guid.Parse(book.Id) }, new AddBookResponse(Guid.Parse(book.Id), StatusCodes.Status201Created));
        }

        /// <summary>
        /// Delete a book
        /// </summary>
        /// <param name="request">Book id</param>
        /// <response code="204">Success - The book has been deleted</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteBook([FromRoute] DeleteBookRequest request)
        {
            await bookRepository.DeleteBook(request.Id.ToString());
            return NoContent();
        }
    }
}