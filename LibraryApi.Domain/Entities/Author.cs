namespace LibraryApi.Domain.Entities;

public class Author
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Email { get; set; }
    public DateTime BirthDate { get; set; }
    public ICollection<Book> Books { get; set; } = new List<Book>();
}
