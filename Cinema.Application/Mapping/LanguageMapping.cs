using onlineCinema.Application.DTOs.Common;
using onlineCinema.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class LanguageMapping
    {
        public partial LanguageDto MapToDto(Language language);

        public partial List<LanguageDto>
            MapToDtoList(IEnumerable<Language> languages);

        public partial Language MapToEntity(LanguageDto dto);

        public partial void UpdateEntityFromDto(
            LanguageDto dto,
            Language entity);
    }
}