using LibraryApi.Application.Books.Dtos;
using LibraryApi.Domain.Entities;

namespace LibraryApi.Application.Books.Mappers;

/// <summary>
/// Maps between the `Book` entity and its DTOs.
/// </summary>
public static class BookMapper
{
    /// <summary>
    /// Maps a <see cref="Book"/> entity to a <see cref="BookDto"/>.
    /// </summary>
    /// <param name="book">The book entity.</param>
    /// <returns>A DTO containing book data.</returns>
    public static BookDto ToDto(this Book book) => new(
        book.Id,
        book.Title,
        book.Description,
        book.ISBN,
        book.PublishedDate,
        book.AuthorId
    );

    /// <summary>
    /// Creates a <see cref="Book"/> entity from a <see cref="CreateBookDto"/>.
    /// </summary>
    /// <param name="dto">The creation DTO.</param>
    /// <returns>A new book entity (time portion of date is truncated).</returns>
    public static Book ToEntity(this CreateBookDto dto) => new()
    {
        Title = dto.Title,
        Description = dto.Description,
        ISBN = dto.ISBN,
        PublishedDate = dto.PublishedDate.Date,
        AuthorId = dto.AuthorId
    };

    /// <summary>
    /// Updates an existing <see cref="Book"/> entity from an <see cref="UpdateBookDto"/>.
    /// </summary>
    /// <param name="dto">The update DTO.</param>
    /// <param name="book">The book entity to update.</param>
    public static void UpdateEntity(this UpdateBookDto dto, Book book)
    {
        book.Title = dto.Title;
        book.Description = dto.Description;
        book.ISBN = dto.ISBN;
        book.PublishedDate = dto.PublishedDate.Date;
        book.AuthorId = dto.AuthorId;
    }
}
