using onlineCinema.Application.DTOs;
using onlineCinema.Domain.Entities;
using Riok.Mapperly.Abstractions;
using System.Collections.Generic;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class LanguageMapping
    {
        public partial LanguageDto MapToDto(SkillLevel language);

        public partial List<LanguageDto> 
            MapToDtoList(IEnumerable<SkillLevel> languages);

        public partial SkillLevel MapToEntity(LanguageDto dto);

        public partial void UpdateEntityFromDto(
            LanguageDto dto, 
            SkillLevel entity);
    }
}