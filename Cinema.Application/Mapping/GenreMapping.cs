using onlineCinema.Application.DTOs;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Application.Mapping;

public class GenreMapping
{
    public GenreDto MapToDto(Genre entity) => new()
    {
        GenreId = entity.GenreId,
        GenreName = entity.GenreName
    };

    public Genre MapToEntity(GenreDto dto) => new()
    {
        GenreId = dto.GenreId,
        GenreName = dto.GenreName
    };

    public void UpdateEntityFromDto(GenreDto dto, Genre entity)
    {
        entity.GenreName = dto.GenreName;
    }
}