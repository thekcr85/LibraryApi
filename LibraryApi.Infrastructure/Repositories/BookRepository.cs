using LibraryApi.Application.Interfaces.Repositories;
using LibraryApi.Domain.Entities;
using LibraryApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Infrastructure.Repositories;

public class BookRepository(ApplicationDbContext applicationDbContext) : IBookRepository
{
	private readonly ApplicationDbContext _db = applicationDbContext;

	/// <inheritdoc/>
	public async Task<IReadOnlyList<Book>> GetAllAsync()
	{
		return await applicationDbContext.Books.ToListAsync();
	}

	/// <inheritdoc/>
	public async Task<Book?> GetByIdAsync(int id)
	{
		return await applicationDbContext.Books.FindAsync(id);
	}

	/// <inheritdoc/>
	public async Task<Book> AddAsync(Book book)
	{
		applicationDbContext.Books.Add(book);
		await applicationDbContext.SaveChangesAsync();
		return book;
	}

	/// <inheritdoc/>
	public async Task<Book> UpdateAsync(Book book)
	{
		var existingBook = await applicationDbContext.Books.FindAsync(book.Id);
		if (existingBook == null)
		{
			throw new KeyNotFoundException($"Book with ID {book.Id} not found.");
		}
		applicationDbContext.Entry(existingBook).CurrentValues.SetValues(book);
		await applicationDbContext.SaveChangesAsync();
		return existingBook;
	}

	/// <inheritdoc/>
	public async Task<bool> DeleteAsync(int id)
	{
		var existingBook = await applicationDbContext.Books.FindAsync(id);
		if (existingBook == null)
		{
			return false;
		}
		applicationDbContext.Books.Remove(existingBook);
		await applicationDbContext.SaveChangesAsync();
		return true;
	}

	/// <inheritdoc/>
	public async Task<Book?> GetByIsbnAsync(string isbn)
	{
		return await applicationDbContext.Books.FirstOrDefaultAsync(b => b.ISBN == isbn);
	}

	/// <inheritdoc/>
	public async Task<IReadOnlyList<Book>> GetByAuthorAsync(int authorId)
	{
		return await applicationDbContext.Books
			.Where(b => b.AuthorId == authorId)
			.ToListAsync();
	}

	/// <inheritdoc/>
	public async Task<IReadOnlyList<Book>> SearchByTitleAsync(string titleFragment)
	{
		return await applicationDbContext.Books
			.Where(b => b.Title.Contains(titleFragment))
			.ToListAsync();
	}

	/// <inheritdoc/>
	public async Task<IReadOnlyList<Book>> GetByPublicationYearAsync(int year)
	{
		return await applicationDbContext.Books
			.Where(b => b.PublishedDate.Year == year)
			.ToListAsync();
	}

	/// <inheritdoc/>
	public async Task<IReadOnlyList<Book>> GetRecent(int count)
	{
		return await applicationDbContext.Books
			.OrderByDescending(b => b.PublishedDate)
			.Take(count) // Take the most recent 'count' books
			.ToListAsync();
	}
}
