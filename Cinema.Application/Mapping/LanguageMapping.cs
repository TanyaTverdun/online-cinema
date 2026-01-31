using onlineCinema.Application.DTOs;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Application.Mapping;

public class LanguageMapping
{
    public LanguageDto MapToDto(Language entity) => new()
    {
        LanguageId = entity.LanguageId,
        LanguageName = entity.LanguageName
    };

    public Language MapToEntity(LanguageDto dto) => new()
    {
        LanguageId = dto.LanguageId,
        LanguageName = dto.LanguageName
    };

    public void UpdateEntityFromDto(LanguageDto dto, Language entity)
    {
        entity.LanguageName = dto.LanguageName;
    }
}