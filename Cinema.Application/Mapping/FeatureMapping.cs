using System.Collections.Generic;
using onlineCinema.Application.DTOs;
using onlineCinema.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class FeatureMapping
    {
        [MapProperty(nameof(Feature.Id), nameof(FeatureDto.FeatureId))]
        [MapProperty(nameof(Feature.Name), nameof(FeatureDto.FeatureName))]
        [MapProperty(nameof(Feature.Description), nameof(FeatureDto.FeatureDescription))]
        [MapperIgnoreSource(nameof(Feature.MovieFeatures))]
        [MapperIgnoreSource(nameof(Feature.HallFeatures))]
        public partial FeatureDto MapToDto(Feature entity);

        [MapProperty(nameof(FeatureDto.FeatureId), nameof(Feature.Id))]
        [MapProperty(nameof(FeatureDto.FeatureName), nameof(Feature.Name))]
        [MapProperty(nameof(FeatureDto.FeatureDescription), nameof(Feature.Description))]
        [MapperIgnoreTarget(nameof(Feature.MovieFeatures))]
        [MapperIgnoreTarget(nameof(Feature.HallFeatures))]
        public partial Feature MapToEntity(FeatureDto dto);

        [MapProperty(nameof(FeatureDto.FeatureName), nameof(Feature.Name))]
        [MapProperty(nameof(FeatureDto.FeatureDescription), nameof(Feature.Description))]

        [MapperIgnoreTarget(nameof(Feature.Id))]
        [MapperIgnoreSource(nameof(FeatureDto.FeatureId))]

        [MapperIgnoreTarget(nameof(Feature.MovieFeatures))]
        [MapperIgnoreTarget(nameof(Feature.HallFeatures))]
        public partial void UpdateEntityFromDto(FeatureDto dto, Feature entity);

        public partial IEnumerable<FeatureDto> MapToDtoList(IEnumerable<Feature> entities);
    }
}