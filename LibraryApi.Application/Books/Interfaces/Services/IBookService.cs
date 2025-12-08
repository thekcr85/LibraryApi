using LibraryApi.Application.Books.Dtos;

namespace LibraryApi.Application.Books.Interfaces.Services;

/// <summary>
/// Provides methods for managing books in the library.
/// </summary>
public interface IBookService
{
	/// <summary>
	/// Gets all books.
	/// </summary>
	/// <param name="cancellationToken">Cancellation token.</param>
	Task<IReadOnlyList<BookDto>> GetAllBooksAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets a book by its identifier.
	/// </summary>
	/// <param name="id">The book identifier.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	Task<BookDto?> GetBookByIdAsync(int id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Creates a new book.
	/// </summary>
	/// <param name="createBookDto">The book creation data.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	Task<BookDto> CreateBookAsync(CreateBookDto createBookDto, CancellationToken cancellationToken = default);

	/// <summary>
	/// Updates an existing book.
	/// </summary>
	/// <param name="id">The book identifier.</param>
	/// <param name="updateBookDto">The book update data.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	Task<BookDto?> UpdateBookAsync(int id, UpdateBookDto updateBookDto, CancellationToken cancellationToken = default);

	/// <summary>
	/// Deletes a book by its identifier.
	/// </summary>
	/// <param name="id">The book identifier.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	Task<bool> DeleteBookAsync(int id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets a book by its ISBN.
	/// </summary>
	/// <param name="isbn">The ISBN to search for.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	Task<BookDto?> GetBookByIsbnAsync(string isbn, CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets books by the specified author.
	/// </summary>
	/// <param name="authorId">The author identifier.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	Task<IReadOnlyList<BookDto>> GetBooksByAuthorAsync(int authorId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Searches books by a fragment of their title.
	/// </summary>
	/// <param name="titleFragment">The title fragment to search for.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	Task<IReadOnlyList<BookDto>> SearchBooksByTitleAsync(string titleFragment, CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets books published in the specified year.
	/// </summary>
	/// <param name="year">The publication year.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	Task<IReadOnlyList<BookDto>> GetBooksByPublicationYearAsync(int year, CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets the most recent books.
	/// </summary>
	/// <param name="count">The maximum number of recent books to return.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	Task<IReadOnlyList<BookDto>> GetRecentBooksAsync(int count, CancellationToken cancellationToken = default);
}
