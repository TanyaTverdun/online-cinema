using onlineCinema.Application.DTOs;
using onlineCinema.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Application.Mapping;

[Mapper]
public partial class HallMapper
{
    public HallDto MapToDto(Hall hall)
    {
        return new HallDto
        {
            Id = hall.HallId,
            HallNumber = hall.HallNumber,
            RowCount = hall.RowCount,
            SeatInRowCount = hall.SeatInRowCount,
            FeatureNames = hall.HallFeatures
                .Select(hf => hf.Feature.Name)
                .ToList(),
            FeatureIds = hall.HallFeatures
                .Select(hf => hf.FeatureId)
                .ToList()
        };
    }

    [MapProperty(nameof(Hall.HallId), nameof(HallDto.Id))]
    public partial HallDto MapToHallDtoBase(Hall hall);

    public HallDto MapToHallDto(Hall hall)
    {
        var dto = MapToHallDtoBase(hall);

        dto.FeatureNames = hall.HallFeatures?
            .Select(hf => hf.Feature.Name)
            .ToList() ?? new();

        dto.FeatureIds = hall.HallFeatures?
            .Select(hf => hf.FeatureId)
            .ToList() ?? new();

        return dto;
    }

    [MapProperty(nameof(HallDto.Id), nameof(Hall.HallId))]
    public partial Hall MapToHallEntityBase(HallDto dto);

    public Hall MapToEntity(HallDto dto)
    {
        return MapToHallEntityBase(dto);
    }
}
