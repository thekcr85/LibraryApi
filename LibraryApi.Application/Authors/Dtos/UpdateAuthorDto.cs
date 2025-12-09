using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Application.Authors.Dtos;

/// <summary>
/// DTO used when updating an existing author.
/// </summary>
public record UpdateAuthorDto(
    int Id,
    [Required, StringLength(100)] string Name,
    [EmailAddress, StringLength(100)] string? Email,
    [DataType(DataType.Date)] DateTime BirthDate
);
