using LibraryApi.Domain.Common;

namespace LibraryApi.Domain.Entities;

/// <summary>
/// Represents a book in the library.
/// </summary>
public class Book : BaseEntity
{
    /// <summary>
    /// The book title.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// A short description or summary of the book.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The ISBN code of the book (if available).
    /// </summary>
    public string? ISBN { get; set; }

    /// <summary>
    /// The publication date (date component is used).
    /// </summary>
    public DateTime PublishedDate { get; set; }

    /// <summary>
    /// Foreign key to the book's author.
    /// </summary>
    public int AuthorId { get; set; }

    /// <summary>
    /// Navigation property to the author of the book.
    /// </summary>
    public Author? Author { get; set; }
}
