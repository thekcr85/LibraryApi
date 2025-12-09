using LibraryApi.Application.Authors.Interfaces.Repositories;
using LibraryApi.Domain.Entities;
using LibraryApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Infrastructure.Repositories;

/// <summary>
/// Repository for managing <see cref="Author"/> entities.
/// </summary>
/// <remarks>All methods are asynchronous. Not thread-safe.</remarks>
/// <param name="applicationDbContext">EF Core <see cref="ApplicationDbContext"/> instance.</param>
public class AuthorRepository(ApplicationDbContext applicationDbContext) : IAuthorRepository
{
	/// <inheritdoc/>
	public async Task<IReadOnlyList<Author>> GetAllAsync(CancellationToken cancellationToken = default)
	{
		return await applicationDbContext.Authors
			.AsNoTracking()
			.ToListAsync(cancellationToken);
	}

	/// <inheritdoc/>
	public async Task<Author?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
	{
		return await applicationDbContext.Authors
			.FindAsync([id], cancellationToken);
	}

	/// <inheritdoc/>
	public async Task<Author> AddAsync(Author author, CancellationToken cancellationToken = default)
	{
		author.CreatedAt = DateTime.UtcNow;
		applicationDbContext.Authors.Add(author);
		await applicationDbContext.SaveChangesAsync(cancellationToken);
		return author;
	}

	/// <inheritdoc/>
	public async Task<Author> UpdateAsync(Author author, CancellationToken cancellationToken = default)
	{
		author.UpdatedAt = DateTime.UtcNow;
		applicationDbContext.Authors.Update(author);
		await applicationDbContext.SaveChangesAsync(cancellationToken);
		return author;
	}

	/// <inheritdoc/>
	public async Task DeleteAsync(Author author, CancellationToken cancellationToken = default)
	{
		applicationDbContext.Authors.Remove(author);
		await applicationDbContext.SaveChangesAsync(cancellationToken);
	}

	/// <inheritdoc/>
	public async Task<IReadOnlyList<Author>> SearchByNameAsync(string nameFragment, CancellationToken cancellationToken = default)
	{
		return await applicationDbContext.Authors
			.AsNoTracking()
			.Where(a => EF.Functions.Like(a.Name, $"%{nameFragment}%"))
			.ToListAsync(cancellationToken);
	}

	/// <inheritdoc/>
	public async Task<Author?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
	{
		return await applicationDbContext.Authors
			.AsNoTracking()
			.FirstOrDefaultAsync(a => a.Email == email, cancellationToken);
	}

	/// <inheritdoc/>
	public async Task<IReadOnlyList<Author>> GetRecentAsync(int count, CancellationToken cancellationToken = default)
	{
		return await applicationDbContext.Authors
			.AsNoTracking()
			.OrderByDescending(a => a.CreatedAt)
			.Take(count)
			.ToListAsync(cancellationToken);
	}
}
