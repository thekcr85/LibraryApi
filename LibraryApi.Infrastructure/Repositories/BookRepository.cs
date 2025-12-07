using LibraryApi.Application.Interfaces.Repositories;
using LibraryApi.Domain.Entities;

namespace LibraryApi.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
	public Task<IReadOnlyList<Book>> GetAllAsync()
	{
		throw new NotImplementedException();
	}

	public Task<Book?> GetByIdAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task<Book> AddAsync(Book book)
	{
		throw new NotImplementedException();
	}

	public Task<Book> UpdateAsync(Book book)
	{
		throw new NotImplementedException();
	}

	public Task<bool> DeleteAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task<Book?> GetByIsbnAsync(string isbn)
	{
		throw new NotImplementedException();
	}

	public Task<IReadOnlyList<Book>> GetByAuthorAsync(int authorId)
	{
		throw new NotImplementedException();
	}

	public Task<IReadOnlyList<Book>> SearchByTitleAsync(string titleFragment)
	{
		throw new NotImplementedException();
	}

	public Task<IReadOnlyList<Book>> GetByPublicationYearAsync(int year)
	{
		throw new NotImplementedException();
	}

	public Task<IReadOnlyList<Book>> GetRecent(int count)
	{
		throw new NotImplementedException();
	}
}
