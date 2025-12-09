using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Application.Authors.Dtos;

/// <summary>
/// Data transfer object for returning author information.
/// </summary>
public record AuthorDto(
    int Id,
    [Required, StringLength(100)] string Name,
    [EmailAddress, StringLength(100)] string? Email,
    [DataType(DataType.Date)] DateTime BirthDate
);
