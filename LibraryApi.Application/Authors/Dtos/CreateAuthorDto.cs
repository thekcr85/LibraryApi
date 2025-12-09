using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Application.Authors.Dtos;

/// <summary>
/// DTO used when creating a new author.
/// </summary>
public record CreateAuthorDto(
    [Required, StringLength(100)] string Name,
    [EmailAddress, StringLength(100)] string? Email,
    [DataType(DataType.Date)] DateTime BirthDate
);
