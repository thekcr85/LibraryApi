using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Application.Books.Dtos;

/// <summary>
/// DTO used when creating a new book.
/// </summary>
public record CreateBookDto(
    [Required, StringLength(100)] string Title,
    string? Description,
    [StringLength(20)] string? ISBN,
    [DataType(DataType.Date)] DateTime PublishedDate,
    int AuthorId
);
