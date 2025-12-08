using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Application.Books.Dtos;

/// <summary>
/// Data transfer object for returning book information.
/// </summary>
public record BookDto(
    int Id,
    [Required, StringLength(100)] string Title,
    string? Description,
    [StringLength(20)] string? ISBN,
    [DataType(DataType.Date)] DateTime PublishedDate,
    int AuthorId
);
