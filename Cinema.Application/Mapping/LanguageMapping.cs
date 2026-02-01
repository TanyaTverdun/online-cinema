using System.Collections.Generic;
using onlineCinema.Application.DTOs;
using onlineCinema.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class LanguageMapping
    {
        [MapperIgnoreSource(nameof(Language.Movies))]
        [MapperIgnoreSource(nameof(Language.LanguageMovies))]
        public partial LanguageDto MapToDto(Language entity);

        [MapperIgnoreTarget(nameof(Language.Movies))]
        [MapperIgnoreTarget(nameof(Language.LanguageMovies))]
        public partial Language MapToEntity(LanguageDto dto);

        [MapperIgnoreTarget(nameof(Language.Movies))]
        [MapperIgnoreTarget(nameof(Language.LanguageMovies))]
        [MapperIgnoreTarget(nameof(Language.LanguageId))]
        [MapperIgnoreSource(nameof(LanguageDto.LanguageId))]
        public partial void UpdateEntityFromDto(LanguageDto dto, Language entity);

        public partial IEnumerable<LanguageDto> MapToDtoList(IEnumerable<Language> entities);
    }
}