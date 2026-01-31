using onlineCinema.Application.DTOs;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Application.Mapping;

public class SnackMapping
{
    public SnackDto MapToDto(Snack entity) => new()
    {
        SnackId = entity.SnackId,
        SnackName = entity.SnackName,
        Price = entity.Price
    };

    public Snack MapToEntity(SnackDto dto) => new()
    {
        SnackId = dto.SnackId,
        SnackName = dto.SnackName,
        Price = dto.Price
    };

    public void UpdateEntityFromDto(SnackDto dto, Snack entity)
    {
        entity.SnackName = dto.SnackName;
        entity.Price = dto.Price;
    }
}