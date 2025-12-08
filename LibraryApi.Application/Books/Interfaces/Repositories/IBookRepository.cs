using LibraryApi.Domain.Entities;

namespace LibraryApi.Application.Books.Interfaces.Repositories;

/// <summary>
/// Repository abstraction for managing <see cref="Book"/> entities.
/// </summary>
public interface IBookRepository
{
	// CRUD operations

	/// <summary>
	/// Gets all books.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>A read-only list of all books.</returns>
	Task<IReadOnlyList<Book>> GetAllAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets a book by its identifier.
	/// </summary>
	/// <param name="id">The book identifier.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>The book if found; otherwise <c>null</c>.</returns>
	Task<Book?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Adds a new book.
	/// </summary>
	/// <param name="book">The book to add.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>The added book (including any database-generated values).</returns>
	Task<Book> AddAsync(Book book, CancellationToken cancellationToken = default);

	/// <summary>
	/// Updates an existing book.
	/// </summary>
	/// <param name="book">The book with updated values.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>The updated book.</returns>
	Task<Book> UpdateAsync(Book book, CancellationToken cancellationToken = default);

	/// <summary>
	/// Deletes the specified book.
	/// </summary>
	/// <param name="book">The book to delete. Cannot be null.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>A task that represents the asynchronous delete operation.</returns>
	Task DeleteAsync(Book book, CancellationToken cancellationToken = default);

	// Additional methods

	/// <summary>
	/// Gets a book by its ISBN.
	/// </summary>
	/// <param name="isbn">The ISBN to search for.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>The book if found; otherwise <c>null</c>.</returns>
	Task<Book?> GetByIsbnAsync(string isbn, CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets books by an author.
	/// </summary>
	/// <param name="authorId">The author identifier.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>A read-only list of books for the given author.</returns>
	Task<IReadOnlyList<Book>> GetByAuthorAsync(int authorId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Searches books by a fragment of their title.
	/// </summary>
	/// <param name="titleFragment">The title fragment to search for.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>A read-only list of matching books.</returns>
	Task<IReadOnlyList<Book>> SearchByTitleAsync(string titleFragment, CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets books published in the specified year.
	/// </summary>
	/// <param name="year">The publication year.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>A read-only list of books published in the given year.</returns>
	Task<IReadOnlyList<Book>> GetByPublicationYearAsync(int year, CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets the most recent books.
	/// </summary>
	/// <param name="count">The maximum number of recent books to return.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>A read-only list of recent books.</returns>
	Task<IReadOnlyList<Book>> GetRecentAsync(int count, CancellationToken cancellationToken = default);
}
