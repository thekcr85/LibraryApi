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
	}
}
