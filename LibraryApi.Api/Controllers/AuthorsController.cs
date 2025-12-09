using LibraryApi.Application.Authors.Dtos;
using LibraryApi.Application.Authors.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Api.Controllers;

/// <summary>
/// API controller for managing authors.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthorsController(IAuthorService authorService) : ControllerBase
{
	/// <summary>
	/// Retrieves all authors.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>200 OK with list of authors.</returns>
	[HttpGet]
	[ProducesResponseType(typeof(IReadOnlyList<AuthorDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetAllAuthors(CancellationToken cancellationToken)
	{
		var authors = await authorService.GetAllAuthorsAsync(cancellationToken);
		return Ok(authors);
	}

	/// <summary>
	/// Retrieves a single author by id.
	/// </summary>
	/// <param name="id">Author identifier.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>200 OK with the author or 404 Not Found.</returns>
	[HttpGet("{id:int}")]
	[ProducesResponseType(typeof(AuthorDto), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<AuthorDto>> GetAuthorById(int id, CancellationToken cancellationToken)
	{
		var author = await authorService.GetAuthorByIdAsync(id, cancellationToken);
		if (author is null)
			return NotFound();

		return Ok(author);
	}

	/// <summary>
	/// Retrieves an author by email.
	/// </summary>
	/// <param name="email">Author email address.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>200 OK with the author or 404 Not Found.</returns>
	[HttpGet("email/{email}")]
	[ProducesResponseType(typeof(AuthorDto), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<AuthorDto>> GetAuthorByEmail(string email, CancellationToken cancellationToken)
	{
		var author = await authorService.GetAuthorByEmailAsync(email, cancellationToken);
		if (author is null)
			return NotFound();

		return Ok(author);
	}

	/// <summary>
	/// Searches authors by name fragment.
	/// </summary>
	/// <param name="name">Name fragment to search.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>200 OK with list of matching authors.</returns>
	[HttpGet("search")]
	[ProducesResponseType(typeof(IReadOnlyList<AuthorDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> SearchAuthorsByName([FromQuery] string name, CancellationToken cancellationToken)
	{
		var authors = await authorService.SearchAuthorsByNameAsync(name, cancellationToken);
		return Ok(authors);
	}

	/// <summary>
	/// Retrieves the most recent authors.
	/// </summary>
	/// <param name="count">Number of recent authors to retrieve (default: 10).</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>200 OK with list of recent authors.</returns>
	[HttpGet("recent")]
	[ProducesResponseType(typeof(IReadOnlyList<AuthorDto>), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetRecentAuthors([FromQuery] int count = 10, CancellationToken cancellationToken = default)
	{
		var authors = await authorService.GetRecentAuthorsAsync(count, cancellationToken);
		return Ok(authors);
	}

	/// <summary>
	/// Creates a new author.
	/// </summary>
	/// <param name="createAuthorDto">Author creation DTO.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>201 Created with the created author.</returns>
	[HttpPost]
	[ProducesResponseType(typeof(AuthorDto), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult<AuthorDto>> CreateAuthor([FromBody] CreateAuthorDto createAuthorDto, CancellationToken cancellationToken)
	{
		var createdAuthor = await authorService.CreateAuthorAsync(createAuthorDto, cancellationToken);
		return CreatedAtAction(nameof(GetAuthorById), new { id = createdAuthor.Id }, createdAuthor);
	}

	/// <summary>
	/// Updates an existing author.
	/// </summary>
	/// <param name="id">Author identifier from URL.</param>
	/// <param name="updateAuthorDto">Update DTO.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>200 OK with updated author, 404 Not Found or 400 Bad Request.</returns>
	[HttpPut("{id:int}")]
	[ProducesResponseType(typeof(AuthorDto), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult<AuthorDto>> UpdateAuthor(int id, [FromBody] UpdateAuthorDto updateAuthorDto, CancellationToken cancellationToken)
	{
		if (id != updateAuthorDto.Id)
			return BadRequest("ID in URL does not match ID in body.");

		var updatedAuthor = await authorService.UpdateAuthorAsync(id, updateAuthorDto, cancellationToken);
		if (updatedAuthor is null)
			return NotFound();

		return Ok(updatedAuthor);
	}

	/// <summary>
	/// Deletes an author by id.
	/// </summary>
	/// <param name="id">Author identifier.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>204 No Content if deleted, 404 Not Found otherwise.</returns>
	[HttpDelete("{id:int}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> DeleteAuthor(int id, CancellationToken cancellationToken)
	{
		var deleted = await authorService.DeleteAuthorAsync(id, cancellationToken);
		if (!deleted)
			return NotFound();

		return NoContent();
	}
}
