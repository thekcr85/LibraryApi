using LibraryApi.Domain.Entities;

namespace LibraryApi.Application.Authors.Interfaces.Repositories;

/// <summary>
/// Repository abstraction for managing <see cref="Author"/> entities.
/// </summary>
public interface IAuthorRepository
{
	// CRUD operations

	/// <summary>
	/// Gets all authors.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>A read-only list of all authors.</returns>
	Task<IReadOnlyList<Author>> GetAllAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets an author by its identifier.
	/// </summary>
	/// <param name="id">The author identifier.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>The author if found; otherwise <c>null</c>.</returns>
	Task<Author?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Adds a new author.
	/// </summary>
	/// <param name="author">The author to add.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>The added author (including any database-generated values).</returns>
	Task<Author> AddAsync(Author author, CancellationToken cancellationToken = default);

	/// <summary>
	/// Updates an existing author.
	/// </summary>
	/// <param name="author">The author with updated values.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>The updated author.</returns>
	Task<Author> UpdateAsync(Author author, CancellationToken cancellationToken = default);

	/// <summary>
	/// Deletes the specified author.
	/// </summary>
	/// <param name="author">The author to delete. Cannot be null.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>A task that represents the asynchronous delete operation.</returns>
	Task DeleteAsync(Author author, CancellationToken cancellationToken = default);

	// Additional methods

	/// <summary>
	/// Gets authors by a fragment of their name.
	/// </summary>
	/// <param name="nameFragment">The name fragment to search for.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>A read-only list of matching authors.</returns>
	Task<IReadOnlyList<Author>> SearchByNameAsync(string nameFragment, CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets authors by email.
	/// </summary>
	/// <param name="email">The email to search for.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>The author if found; otherwise <c>null</c>.</returns>
	Task<Author?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets the most recent authors.
	/// </summary>
	/// <param name="count">The maximum number of recent authors to return.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>A read-only list of recent authors.</returns>
	Task<IReadOnlyList<Author>> GetRecentAsync(int count, CancellationToken cancellationToken = default);
}
