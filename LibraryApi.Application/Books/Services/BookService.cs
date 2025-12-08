using LibraryApi.Application.Books.Dtos;
using LibraryApi.Application.Books.Interfaces.Repositories;
using LibraryApi.Application.Books.Interfaces.Services;
using LibraryApi.Application.Books.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryApi.Application.Books.Services
{
	/// <summary>
	/// Provides services for managing books
	/// </summary>
	public class BookService(IBookRepository bookRepository) : IBookService
	{
		/// <inheritdoc/>
		public async Task<IReadOnlyList<BookDto>> GetAllBooksAsync()
		{
			var books = await bookRepository.GetAllAsync();
			if (books is null || books.Count == 0)
				return Array.Empty<BookDto>();

			return books.Select(m => m.ToDto()).ToList();
		}

		/// <inheritdoc/>
		public async Task<BookDto?> GetBookByIdAsync(int id)
		{
			var book = await bookRepository.GetByIdAsync(id);
			return book?.ToDto();
		}

		/// <inheritdoc/>
		public async Task<BookDto> CreateBookAsync(CreateBookDto createBookDto)
		{
			var book = createBookDto.ToEntity();
			var createdBook = await bookRepository.AddAsync(book);
			return createdBook.ToDto();
		}

		/// <inheritdoc/>
		public Task<BookDto> UpdateBookAsync(int id, UpdateBookDto updateBookDto)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public Task<bool> DeleteBookAsync(int id)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public Task<BookDto?> GetBookByIsbnAsync(string isbn)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public Task<IReadOnlyList<BookDto>> GetBooksByAuthorAsync(int authorId)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public Task<IReadOnlyList<BookDto>> SearchBooksByTitleAsync(string titleFragment)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public Task<IReadOnlyList<BookDto>> GetBooksByPublicationYearAsync(int year)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public Task<IReadOnlyList<BookDto>> GetRecentBooksAsync(int count)
		{
			throw new NotImplementedException();
		}
	}
}
