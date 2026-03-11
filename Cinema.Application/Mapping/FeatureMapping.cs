using onlineCinema.Application.DTOs.Movie;
using onlineCinema.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class FeatureMapping
    {
        public partial FeatureDto MapToDto(Feature feature);

        public partial List<FeatureDto>
            MapToDtoList(IEnumerable<Feature> features);

        public partial Feature MapToEntity(FeatureDto dto);

        public partial void UpdateEntityFromDto(
            FeatureDto dto,
            Feature entity);
    }
}