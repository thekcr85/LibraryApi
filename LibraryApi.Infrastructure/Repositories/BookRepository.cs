using LibraryApi.Application.Books.Interfaces.Repositories;
using LibraryApi.Domain.Entities;
using LibraryApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Infrastructure.Repositories;

/// <summary>
/// Repository for managing <see cref="Book"/> entities.
/// </summary>
/// <remarks>All methods are asynchronous. Not thread-safe.</remarks>
/// <param name="applicationDbContext">EF Core <see cref="ApplicationDbContext"/> instance.</param>
public class BookRepository(ApplicationDbContext applicationDbContext) : IBookRepository
{
	/// <inheritdoc/>
	public async Task<IReadOnlyList<Book>> GetAllAsync(CancellationToken cancellationToken = default)
	{
		return await applicationDbContext.Books
			.AsNoTracking()
			.ToListAsync(cancellationToken);
	}

	/// <inheritdoc/>
	public async Task<Book?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
	{
		return await applicationDbContext.Books
			.FindAsync([id], cancellationToken);
	}

	/// <inheritdoc/>
	public async Task<Book> AddAsync(Book book, CancellationToken cancellationToken = default)
	{
		book.CreatedAt = DateTime.UtcNow;
		applicationDbContext.Books.Add(book);
		await applicationDbContext.SaveChangesAsync(cancellationToken);
		return book;
	}

	/// <inheritdoc/>
	public async Task<Book> UpdateAsync(Book book, CancellationToken cancellationToken = default)
	{
		book.UpdatedAt = DateTime.UtcNow;
		applicationDbContext.Books.Update(book);
		await applicationDbContext.SaveChangesAsync(cancellationToken);
		return book;
	}

	/// <inheritdoc/>
	public async Task DeleteAsync(Book book, CancellationToken cancellationToken = default)
	{ 
		applicationDbContext.Books.Remove(book);
		await applicationDbContext.SaveChangesAsync(cancellationToken);
	}

	/// <inheritdoc/>
	public async Task<Book?> GetByIsbnAsync(string isbn, CancellationToken cancellationToken = default)
	{
		return await applicationDbContext.Books
			.AsNoTracking()
			.FirstOrDefaultAsync(b => b.ISBN == isbn, cancellationToken);
	}

	/// <inheritdoc/>
	public async Task<IReadOnlyList<Book>> GetByAuthorAsync(int authorId, CancellationToken cancellationToken = default)
	{
		return await applicationDbContext.Books
			.AsNoTracking()
			.Where(b => b.AuthorId == authorId)
			.ToListAsync(cancellationToken);
	}

	/// <inheritdoc/>
	public async Task<IReadOnlyList<Book>> SearchByTitleAsync(string titleFragment, CancellationToken cancellationToken = default)
	{
		return await applicationDbContext.Books
			.AsNoTracking()
			.Where(b => EF.Functions.Like(b.Title, $"%{titleFragment}%"))
			.ToListAsync(cancellationToken);
	}

	/// <inheritdoc/>
	public async Task<IReadOnlyList<Book>> GetByPublicationYearAsync(int year, CancellationToken cancellationToken = default)
	{
		return await applicationDbContext.Books
			.AsNoTracking()
			.Where(b => b.PublishedDate.Year == year)
			.ToListAsync(cancellationToken);
	}

	/// <inheritdoc/>
	public async Task<IReadOnlyList<Book>> GetRecentAsync(int count, CancellationToken cancellationToken = default)
	{
		return await applicationDbContext.Books
			.AsNoTracking()
			.OrderByDescending(b => b.PublishedDate)
			.Take(count)
			.ToListAsync(cancellationToken);
	}
}
