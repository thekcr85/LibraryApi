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
	}
}
