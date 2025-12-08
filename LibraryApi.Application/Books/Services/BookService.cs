using LibraryApi.Application.Books.Dtos;
using LibraryApi.Application.Books.Interfaces.Repositories;
using LibraryApi.Application.Books.Interfaces.Services;
using LibraryApi.Application.Books.Mappers;

namespace LibraryApi.Application.Books.Services
{
	public class BookService(IBookRepository bookRepository) : IBookService
	{
		public async Task<IReadOnlyList<BookDto>> GetAllBooksAsync()
		{
			var books = await bookRepository.GetAllAsync();
			if (books == null || books.Count == 0)
			{
				return new List<BookDto>();
			}
			return books.Select(m => m.ToDto()).ToList();
		}

		public Task<BookDto?> GetBookByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<BookDto> CreateBookAsync(CreateBookDto createBookDto)
		{
			throw new NotImplementedException();
		}

		public Task<BookDto> UpdateBookAsync(int id, UpdateBookDto updateBookDto)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteBookAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<BookDto?> GetBookByIsbnAsync(string isbn)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyList<BookDto>> GetBooksByAuthorAsync(int authorId)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyList<BookDto>> SearchBooksByTitleAsync(string titleFragment)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyList<BookDto>> GetBooksByPublicationYearAsync(int year)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyList<BookDto>> GetRecentBooksAsync(int count)
		{
			throw new NotImplementedException();
		}
	}
}
