using LibraryApi.Application.Books.Dtos;
using LibraryApi.Application.Books.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	/// <summary>
	/// API controller for managing books.
	/// </summary>
	public class BooksController(IBookService bookService) : ControllerBase
	{
		/// <summary>
		/// Retrieves all books.
		/// </summary>
		/// <returns>200 OK with list of books.</returns>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAllBooks()
		{
			var books = await bookService.GetAllBooksAsync();
			return Ok(books);
		}

		/// <summary>
		/// Retrieves a single book by id.
		/// </summary>
		/// <param name="id">Book identifier.</param>
		/// <returns>200 OK with the book or 404 Not Found.</returns>
		[HttpGet("{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<BookDto>> GetBookById(int id)
		{
			var book = await bookService.GetBookByIdAsync(id);
			if (book is null)
				return NotFound();
			return Ok(book);
		}

		/// <summary>
		/// Creates a new book.
		/// </summary>
		/// <param name="createBookDto">Book creation DTO.</param>
		/// <returns>201 Created with the created book.</returns>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public async Task<ActionResult<BookDto>> CreateBook([FromBody] CreateBookDto createBookDto)
		{
			var createdBook = await bookService.CreateBookAsync(createBookDto);
			return CreatedAtAction(nameof(GetBookById), new { id = createdBook.Id }, createdBook);
		}

		/// <summary>
		/// Updates an existing book.
		/// </summary>
		/// <param name="id">Book identifier from URL.</param>
		/// <param name="updateBookDto">Update DTO.</param>
		/// <returns>200 OK with updated book, 404 Not Found or 400 Bad Request.</returns>
		[HttpPut("{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<BookDto>> UpdateBook(int id, [FromBody] UpdateBookDto updateBookDto)
		{
			if (id != updateBookDto.Id)
				return BadRequest("ID in URL does not match ID in body.");
			var updatedBook = await bookService.UpdateBookAsync(id, updateBookDto);
			if (updatedBook is null)
				return NotFound();
			return Ok(updatedBook);
		}

		/// <summary>
		/// Deletes a book by id.
		/// </summary>
		/// <param name="id">Book identifier.</param>
		/// <returns>204 No Content if deleted, 404 Not Found otherwise.</returns>
		[HttpDelete("{id:int}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> DeleteBook(int id)
		{
			var deleted = await bookService.DeleteBookAsync(id);
			if (!deleted)
				return NotFound();
			return NoContent();
		}
	}
}
