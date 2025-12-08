namespace LibraryApi.Application.Books.Dtos;

/// <summary>
/// DTO used when creating a new book.
/// </summary>
public record CreateBookDto(
    string Title,
    string? Description,
    string? ISBN,
    DateTime PublishedDate,
    int AuthorId
);
