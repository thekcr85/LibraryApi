using LibraryApi.Application.Books.Dtos;
using LibraryApi.Application.Books.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BooksController(IBookService bookService) : ControllerBase
	{
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAllBooks()
		{
			var books = await bookService.GetAllBooksAsync();
			return Ok(books);
		}

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

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public async Task<ActionResult<BookDto>> CreateBook([FromBody] CreateBookDto createBookDto)
		{
			var createdBook = await bookService.CreateBookAsync(createBookDto);
			return CreatedAtAction(nameof(GetBookById), new { id = createdBook.Id }, createdBook);
		}

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
