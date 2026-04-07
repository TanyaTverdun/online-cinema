using onlineCinema.Application.DTOs;
using onlineCinema.Application.DTOs.Movie;
using onlineCinema.Domain.Entities;
using Riok.Mapperly.Abstractions;
using System.Collections.Generic;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class FeatureMapping
    {
        public partial FeatureDto MapToDto(Requriment feature);

        public partial List<FeatureDto> 
            MapToDtoList(IEnumerable<Requriment> features);

        public partial Requriment MapToEntity(FeatureDto dto);

        public partial void UpdateEntityFromDto(
            FeatureDto dto, 
            Requriment entity);
    }
}