namespace LibraryApi.Application.Books.Dtos;

/// <summary>
/// DTO used when updating an existing book.
/// </summary>
public record UpdateBookDto(
    int Id,
    string Title,
    string? Description,
    string? ISBN,
    DateTime PublishedDate,
    int AuthorId
);
