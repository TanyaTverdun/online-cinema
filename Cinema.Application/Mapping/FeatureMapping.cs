using onlineCinema.Application.DTOs;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Application.Mapping;

public class FeatureMapping
{
    public FeatureDto MapToDto(Feature entity) => new()
    {
        FeatureId = entity.Id,
        FeatureName = entity.Name,
        FeatureDescription = entity.Description
    };

    public Feature MapToEntity(FeatureDto dto) => new()
    {
        Id = dto.FeatureId,
        Name = dto.FeatureName,
        Description = dto.FeatureDescription
    };

    public void UpdateEntityFromDto(FeatureDto dto, Feature entity)
    {
        entity.Name = dto.FeatureName;
        entity.Description = dto.FeatureDescription;
    }
}