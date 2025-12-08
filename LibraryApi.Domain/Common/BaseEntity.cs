namespace LibraryApi.Domain.Common;

/// <summary>
/// Base class for all domain entities with common properties.
/// </summary>
public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
