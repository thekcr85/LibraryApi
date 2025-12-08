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
	Task<IReadOnlyList<BookDto>> GetAllBooksAsync();

	/// <summary>
	/// Gets a book by its identifier.
	/// </summary>
	Task<BookDto?> GetBookByIdAsync(int id);

	/// <summary>
	/// Creates a new book.
	/// </summary>
	Task<BookDto> CreateBookAsync(CreateBookDto createBookDto);

	/// <summary>
	/// Updates an existing book.
	/// </summary>
	Task<BookDto> UpdateBookAsync(int id, UpdateBookDto updateBookDto);

	/// <summary>
	/// Deletes a book by its identifier.
	/// </summary>
	Task<bool> DeleteBookAsync(int id);

	/// <summary>
	/// Gets a book by its ISBN.
	/// </summary>
	Task<BookDto?> GetBookByIsbnAsync(string isbn);

	/// <summary>
	/// Gets books by the specified author.
	/// </summary>
	Task<IReadOnlyList<BookDto>> GetBooksByAuthorAsync(int authorId);

	/// <summary>
	/// Searches books by a fragment of their title.
	/// </summary>
	Task<IReadOnlyList<BookDto>> SearchBooksByTitleAsync(string titleFragment);

	/// <summary>
	/// Gets books published in the specified year.
	/// </summary>
	Task<IReadOnlyList<BookDto>> GetBooksByPublicationYearAsync(int year);

	/// <summary>
	/// Gets the most recent books.
	/// </summary>
	Task<IReadOnlyList<BookDto>> GetRecentBooksAsync(int count);


}
