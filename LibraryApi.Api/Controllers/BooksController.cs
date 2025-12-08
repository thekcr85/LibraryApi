using LibraryApi.Application.Books.Dtos;
using LibraryApi.Application.Books.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Api.Controllers;

/// <summary>
/// API controller for managing books.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class BooksController(IBookService bookService) : ControllerBase
{
	/// <summary>
	/// Retrieves all books.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>200 OK with list of books.</returns>
	[HttpGet]
	[ProducesResponseType(typeof(IReadOnlyList<BookDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetAllBooks(CancellationToken cancellationToken)
	{
		var books = await bookService.GetAllBooksAsync(cancellationToken);
		return Ok(books);
	}

	/// <summary>
	/// Retrieves a single book by id.
	/// </summary>
	/// <param name="id">Book identifier.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>200 OK with the book or 404 Not Found.</returns>
	[HttpGet("{id:int}")]
	[ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<BookDto>> GetBookById(int id, CancellationToken cancellationToken)
	{
		var book = await bookService.GetBookByIdAsync(id, cancellationToken);
		if (book is null)
			return NotFound();
		return Ok(book);
	}

	/// <summary>
	/// Retrieves a book by ISBN.
	/// </summary>
	/// <param name="isbn">ISBN code.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>200 OK with the book or 404 Not Found.</returns>
	[HttpGet("isbn/{isbn}")]
	[ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<BookDto>> GetBookByIsbn(string isbn, CancellationToken cancellationToken)
	{
		var book = await bookService.GetBookByIsbnAsync(isbn, cancellationToken);
		if (book is null)
			return NotFound();
		return Ok(book);
	}

	/// <summary>
	/// Retrieves books by author.
	/// </summary>
	/// <param name="authorId">Author identifier.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>200 OK with list of books.</returns>
	[HttpGet("author/{authorId:int}")]
	[ProducesResponseType(typeof(IReadOnlyList<BookDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetBooksByAuthor(int authorId, CancellationToken cancellationToken)
	{
		var books = await bookService.GetBooksByAuthorAsync(authorId, cancellationToken);
		return Ok(books);
	}

	/// <summary>
	/// Searches books by title fragment.
	/// </summary>
	/// <param name="title">Title fragment to search.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>200 OK with list of matching books.</returns>
	[HttpGet("search")]
	[ProducesResponseType(typeof(IReadOnlyList<BookDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> SearchBooksByTitle([FromQuery] string title, CancellationToken cancellationToken)
	{
		var books = await bookService.SearchBooksByTitleAsync(title, cancellationToken);
		return Ok(books);
	}

	/// <summary>
	/// Retrieves books published in a specific year.
	/// </summary>
	/// <param name="year">Publication year.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>200 OK with list of books.</returns>
	[HttpGet("year/{year:int}")]
	[ProducesResponseType(typeof(IReadOnlyList<BookDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetBooksByYear(int year, CancellationToken cancellationToken)
	{
		var books = await bookService.GetBooksByPublicationYearAsync(year, cancellationToken);
		return Ok(books);
	}

	/// <summary>
	/// Retrieves the most recent books.
	/// </summary>
	/// <param name="count">Number of recent books to retrieve (default: 10).</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>200 OK with list of recent books.</returns>
	[HttpGet("recent")]
	[ProducesResponseType(typeof(IReadOnlyList<BookDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetRecentBooks([FromQuery] int count = 10, CancellationToken cancellationToken = default)
	{
		var books = await bookService.GetRecentBooksAsync(count, cancellationToken);
		return Ok(books);
	}

	/// <summary>
	/// Creates a new book.
	/// </summary>
	/// <param name="createBookDto">Book creation DTO.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>201 Created with the created book.</returns>
	[HttpPost]
	[ProducesResponseType(typeof(BookDto), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult<BookDto>> CreateBook([FromBody] CreateBookDto createBookDto, CancellationToken cancellationToken)
	{
		var createdBook = await bookService.CreateBookAsync(createBookDto, cancellationToken);
		return CreatedAtAction(nameof(GetBookById), new { id = createdBook.Id }, createdBook);
	}

	/// <summary>
	/// Updates an existing book.
	/// </summary>
	/// <param name="id">Book identifier from URL.</param>
	/// <param name="updateBookDto">Update DTO.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>200 OK with updated book, 404 Not Found or 400 Bad Request.</returns>
	[HttpPut("{id:int}")]
	[ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult<BookDto>> UpdateBook(int id, [FromBody] UpdateBookDto updateBookDto, CancellationToken cancellationToken)
	{
		if (id != updateBookDto.Id)
			return BadRequest("ID in URL does not match ID in body.");
		
		var updatedBook = await bookService.UpdateBookAsync(id, updateBookDto, cancellationToken);
		if (updatedBook is null)
			return NotFound();
		
		return Ok(updatedBook);
	}

	/// <summary>
	/// Deletes a book by id.
	/// </summary>
	/// <param name="id">Book identifier.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>204 No Content if deleted, 404 Not Found otherwise.</returns>
	[HttpDelete("{id:int}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> DeleteBook(int id, CancellationToken cancellationToken)
	{
		var deleted = await bookService.DeleteBookAsync(id, cancellationToken);
		if (!deleted)
			return NotFound();
		
		return NoContent();
	}
}
