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
			if (createBookDto is null)
				throw new ArgumentNullException(nameof(createBookDto));
			var bookEntity = BookMapper.ToEntity(createBookDto);
			var resultBook = await bookRepository.AddAsync(bookEntity);
			return resultBook.ToDto();
		}

		/// <inheritdoc/>
		public async Task<BookDto?> UpdateBookAsync(int id, UpdateBookDto updateBookDto)
		{
			if (updateBookDto is null)
				throw new ArgumentNullException(nameof(updateBookDto));

			var existingBook = await bookRepository.GetByIdAsync(id);
			if (existingBook is null)
				return null;

			BookMapper.UpdateEntity(updateBookDto, existingBook);
			var resultBook = await bookRepository.UpdateAsync(existingBook);
			return resultBook.ToDto();
		}

		/// <inheritdoc/>
		public async Task<bool> DeleteBookAsync(int id)
		{
			var existingBook = await bookRepository.GetByIdAsync(id);
			if (existingBook is null)
				return false;
			await bookRepository.DeleteAsync(existingBook);
			return true;
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
