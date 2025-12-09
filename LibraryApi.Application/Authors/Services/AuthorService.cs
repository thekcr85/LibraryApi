using LibraryApi.Application.Authors.Dtos;
using LibraryApi.Application.Authors.Interfaces.Repositories;
using LibraryApi.Application.Authors.Interfaces.Services;
using LibraryApi.Application.Authors.Mappers;

namespace LibraryApi.Application.Authors.Services;

/// <summary>
/// Provides services for managing authors.
/// </summary>
public class AuthorService(IAuthorRepository authorRepository) : IAuthorService
{
	/// <inheritdoc/>
	public async Task<IReadOnlyList<AuthorDto>> GetAllAuthorsAsync(CancellationToken cancellationToken = default)
	{
		var authors = await authorRepository.GetAllAsync(cancellationToken);
		if (authors is null || authors.Count == 0)
			return []; // Return an empty list if no authors are found

		return authors.Select(a => a.ToDto()).ToList();
	}

	/// <inheritdoc/>
	public async Task<AuthorDto?> GetAuthorByIdAsync(int id, CancellationToken cancellationToken = default)
	{
		var author = await authorRepository.GetByIdAsync(id, cancellationToken);
		return author?.ToDto();
	}

	/// <inheritdoc/>
	public async Task<AuthorDto> CreateAuthorAsync(CreateAuthorDto createAuthorDto, CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(createAuthorDto);

		var authorEntity = AuthorMapper.ToEntity(createAuthorDto);
		var resultAuthor = await authorRepository.AddAsync(authorEntity, cancellationToken);
		return resultAuthor.ToDto();
	}

	/// <inheritdoc/>
	public async Task<AuthorDto?> UpdateAuthorAsync(int id, UpdateAuthorDto updateAuthorDto, CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(updateAuthorDto);

		var existingAuthor = await authorRepository.GetByIdAsync(id, cancellationToken);
		if (existingAuthor is null)
			return null;

		AuthorMapper.UpdateEntity(updateAuthorDto, existingAuthor);
		var resultAuthor = await authorRepository.UpdateAsync(existingAuthor, cancellationToken);
		return resultAuthor.ToDto();
	}

	/// <inheritdoc/>
	public async Task<bool> DeleteAuthorAsync(int id, CancellationToken cancellationToken = default)
	{
		var existingAuthor = await authorRepository.GetByIdAsync(id, cancellationToken);
		if (existingAuthor is null)
			return false;

		await authorRepository.DeleteAsync(existingAuthor, cancellationToken);
		return true;
	}

	/// <inheritdoc/>
	public async Task<IReadOnlyList<AuthorDto>> SearchAuthorsByNameAsync(string nameFragment, CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(nameFragment);

		var authors = await authorRepository.SearchByNameAsync(nameFragment, cancellationToken);
		return authors.Select(a => a.ToDto()).ToList();
	}

	/// <inheritdoc/>
	public async Task<AuthorDto?> GetAuthorByEmailAsync(string email, CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(email);

		var author = await authorRepository.GetByEmailAsync(email, cancellationToken);
		return author?.ToDto();
	}

	/// <inheritdoc/>
	public async Task<IReadOnlyList<AuthorDto>> GetRecentAuthorsAsync(int count, CancellationToken cancellationToken = default)
	{
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

		var authors = await authorRepository.GetRecentAsync(count, cancellationToken);
		return authors.Select(a => a.ToDto()).ToList();
	}
}
