using LibraryApi.Domain.Common;

namespace LibraryApi.Domain.Entities;

/// <summary>
/// Represents an author of books in the library.
/// </summary>
public class Author : BaseEntity
{
    /// <summary>
    /// The author's full name.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Contact email for the author (optional).
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// The author's date of birth (date component is used).
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Navigation collection of books written by the author.
    /// </summary>
    public ICollection<Book> Books { get; set; } = [];
}
