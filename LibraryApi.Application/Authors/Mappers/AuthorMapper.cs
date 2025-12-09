using LibraryApi.Application.Authors.Dtos;
using LibraryApi.Domain.Entities;

namespace LibraryApi.Application.Authors.Mappers;

/// <summary>
/// Maps between the <see cref="Author"/> entity and its DTOs.
/// </summary>
public static class AuthorMapper
{
    /// <summary>
    /// Maps an <see cref="Author"/> entity to an <see cref="AuthorDto"/>.
    /// </summary>
    /// <param name="author">The author entity.</param>
    /// <returns>A DTO containing author data.</returns>
    public static AuthorDto ToDto(this Author author) => new(
        author.Id,
        author.Name,
        author.Email,
        author.BirthDate
    );

    /// <summary>
    /// Creates an <see cref="Author"/> entity from a <see cref="CreateAuthorDto"/>.
    /// </summary>
    /// <param name="dto">The creation DTO.</param>
    /// <returns>A new author entity (time portion of date is truncated).</returns>
    public static Author ToEntity(this CreateAuthorDto dto) => new()
    {
        Name = dto.Name,
        Email = dto.Email,
        BirthDate = dto.BirthDate.Date
    };

    /// <summary>
    /// Updates an existing <see cref="Author"/> entity from an <see cref="UpdateAuthorDto"/>.
    /// </summary>
    /// <param name="dto">The update DTO.</param>
    /// <param name="author">The author entity to update.</param>
    public static void UpdateEntity(this UpdateAuthorDto dto, Author author)
    {
        author.Name = dto.Name;
        author.Email = dto.Email;
        author.BirthDate = dto.BirthDate.Date;
    }
}
