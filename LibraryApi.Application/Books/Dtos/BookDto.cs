namespace LibraryApi.Application.Books.Dtos;

/// <summary>
/// Data transfer object for returning book information.
/// </summary>
public record BookDto(
    int Id,
    string Title,
    string? Description,
    string? ISBN,
    DateTime PublishedDate,
    int AuthorId
);
