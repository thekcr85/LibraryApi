namespace LibraryApi.Domain.Entities;

public class Book
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? ISBN { get; set; }
    public DateTime PublishedDate { get; set; }
    public int AuthorId { get; set; }
    public Author? Author { get; set; }
}
