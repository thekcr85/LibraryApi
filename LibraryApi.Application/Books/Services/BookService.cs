using LibraryApi.Application.Books.Dtos;
using LibraryApi.Application.Books.Interfaces.Repositories;
using LibraryApi.Application.Books.Interfaces.Services;
using LibraryApi.Application.Books.Mappers;

namespace LibraryApi.Application.Books.Services;

/// <summary>
/// Provides services for managing books
/// </summary>
public class BookService(IBookRepository bookRepository) : IBookService
{
	/// <inheritdoc/>
	public async Task<IReadOnlyList<BookDto>> GetAllBooksAsync(CancellationToken cancellationToken = default)
	{
		var books = await bookRepository.GetAllAsync(cancellationToken);
		if (books is null || books.Count == 0)
			return [];

		return books.Select(m => m.ToDto()).ToList();
	}

	/// <inheritdoc/>
	public async Task<BookDto?> GetBookByIdAsync(int id, CancellationToken cancellationToken = default)
	{
		var book = await bookRepository.GetByIdAsync(id, cancellationToken);
		return book?.ToDto();
	}

	/// <inheritdoc/>
	public async Task<BookDto> CreateBookAsync(CreateBookDto createBookDto, CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(createBookDto);
		
		var bookEntity = BookMapper.ToEntity(createBookDto);
		var resultBook = await bookRepository.AddAsync(bookEntity, cancellationToken);
		return resultBook.ToDto();
	}

	/// <inheritdoc/>
	public async Task<BookDto?> UpdateBookAsync(int id, UpdateBookDto updateBookDto, CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(updateBookDto);

		var existingBook = await bookRepository.GetByIdAsync(id, cancellationToken);
		if (existingBook is null)
			return null;

		BookMapper.UpdateEntity(updateBookDto, existingBook);
		var resultBook = await bookRepository.UpdateAsync(existingBook, cancellationToken);
		return resultBook.ToDto();
	}

	/// <inheritdoc/>
	public async Task<bool> DeleteBookAsync(int id, CancellationToken cancellationToken = default)
	{
		var existingBook = await bookRepository.GetByIdAsync(id, cancellationToken);
		if (existingBook is null)
			return false;
		await bookRepository.DeleteAsync(existingBook, cancellationToken);
		return true;
	}

	/// <inheritdoc/>
	public async Task<BookDto?> GetBookByIsbnAsync(string isbn, CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(isbn);
		
		var book = await bookRepository.GetByIsbnAsync(isbn, cancellationToken);
		return book?.ToDto();
	}

	/// <inheritdoc/>
	public async Task<IReadOnlyList<BookDto>> GetBooksByAuthorAsync(int authorId, CancellationToken cancellationToken = default)
	{
		var books = await bookRepository.GetByAuthorAsync(authorId, cancellationToken);
		return books.Select(b => b.ToDto()).ToList();
	}

	/// <inheritdoc/>
	public async Task<IReadOnlyList<BookDto>> SearchBooksByTitleAsync(string titleFragment, CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(titleFragment);
		
		var books = await bookRepository.SearchByTitleAsync(titleFragment, cancellationToken);
		return books.Select(b => b.ToDto()).ToList();
	}

	/// <inheritdoc/>
	public async Task<IReadOnlyList<BookDto>> GetBooksByPublicationYearAsync(int year, CancellationToken cancellationToken = default)
	{
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(year);
		
		var books = await bookRepository.GetByPublicationYearAsync(year, cancellationToken);
		return books.Select(b => b.ToDto()).ToList();
	}

	/// <inheritdoc/>
	public async Task<IReadOnlyList<BookDto>> GetRecentBooksAsync(int count, CancellationToken cancellationToken = default)
	{
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
		
		var books = await bookRepository.GetRecentAsync(count, cancellationToken);
		return books.Select(b => b.ToDto()).ToList();
	}
}
