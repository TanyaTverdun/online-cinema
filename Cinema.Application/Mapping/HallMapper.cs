using onlineCinema.Application.DTOs;
using onlineCinema.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class HallMapper
    {
        private List<int> MapFeaturesToIds(ICollection<HallEquipment> features)
            => features.Select(f => f.FeatureId).ToList();

        [MapProperty(nameof(DanceHall.HallFeatures), 
            nameof(HallDto.FeatureIds), 
            Use = nameof(MapFeaturesToIds))]

        [MapProperty(nameof(DanceHall.HallId), nameof(HallDto.Id))]
        private partial HallDto MapToHallDtoBase(DanceHall hall);

        public HallDto MapToHallDto(DanceHall hall)
        {
            var dto = MapToHallDtoBase(hall);

            dto.FeatureNames = hall.HallFeatures?
                .Select(hf => hf.Feature.Name)
                .ToList() ?? new List<string>();
            if (hall.HallFeatures != null && hall.HallFeatures.Any())
            {
                dto.FeatureIds = hall.HallFeatures
                    .Select(hf => hf.FeatureId).ToList();
            }

            return dto;
        }

        public partial IEnumerable<HallDto> MapToDtoList(IEnumerable<DanceHall> hall);

        [MapProperty(nameof(HallDto.Id), nameof(DanceHall.HallId))]
        private partial DanceHall MapToHallEntityBase(HallDto dto);

        public DanceHall MapToEntity(HallDto dto)
        {
            var entity = MapToHallEntityBase(dto);

            entity.HallFeatures = new List<HallEquipment>();
            entity.Seats = new List<Inventary>();
            entity.CinemaId = 1;

            return entity;
        }
    }
}