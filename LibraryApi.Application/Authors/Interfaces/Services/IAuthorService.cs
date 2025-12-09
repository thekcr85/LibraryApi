using LibraryApi.Application.Authors.Dtos;

namespace LibraryApi.Application.Authors.Interfaces.Services;

/// <summary>
/// Provides methods for managing authors in the library.
/// </summary>
public interface IAuthorService
{
	/// <summary>
	/// Gets all authors.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	Task<IReadOnlyList<AuthorDto>> GetAllAuthorsAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets an author by its identifier.
	/// </summary>
	/// <param name="id">The author identifier.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	Task<AuthorDto?> GetAuthorByIdAsync(int id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Creates a new author.
	/// </summary>
	/// <param name="createAuthorDto">The author creation data.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	Task<AuthorDto> CreateAuthorAsync(CreateAuthorDto createAuthorDto, CancellationToken cancellationToken = default);

	/// <summary>
	/// Updates an existing author.
	/// </summary>
	/// <param name="id">The author identifier.</param>
	/// <param name="updateAuthorDto">The author update data.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	Task<AuthorDto?> UpdateAuthorAsync(int id, UpdateAuthorDto updateAuthorDto, CancellationToken cancellationToken = default);

	/// <summary>
	/// Deletes an author by its identifier.
	/// </summary>
	/// <param name="id">The author identifier.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	Task<bool> DeleteAuthorAsync(int id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Searches authors by a fragment of their name.
	/// </summary>
	/// <param name="nameFragment">The name fragment to search for.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	Task<IReadOnlyList<AuthorDto>> SearchAuthorsByNameAsync(string nameFragment, CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets an author by their email.
	/// </summary>
	/// <param name="email">The email address.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	Task<AuthorDto?> GetAuthorByEmailAsync(string email, CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets the most recent authors.
	/// </summary>
	/// <param name="count">The maximum number of recent authors to return.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	Task<IReadOnlyList<AuthorDto>> GetRecentAuthorsAsync(int count, CancellationToken cancellationToken = default);
}
