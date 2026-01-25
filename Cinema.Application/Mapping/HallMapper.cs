using onlineCinema.Application.DTOs;
using onlineCinema.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class HallMapper
    {
        [MapProperty(nameof(Hall.HallId), nameof(HallDto.Id))]
        private partial HallDto MapToHallDtoBase(Hall hall);

        public HallDto MapToHallDto(Hall hall)
        {
            var dto = MapToHallDtoBase(hall);

            dto.FeatureNames = hall.HallFeatures?
                .Select(hf => hf.Feature.Name)
                .ToList() ?? new List<string>();

            return dto;
        }

        [MapProperty(nameof(HallDto.Id), nameof(Hall.HallId))]
        private partial Hall MapToHallEntityBase(HallDto dto);

        public Hall MapToEntity(HallDto dto)
        {
            var entity = MapToHallEntityBase(dto);

            entity.HallFeatures = new List<HallFeature>();
            entity.Seats = new List<Seat>();

            return entity;
        }
    }
}