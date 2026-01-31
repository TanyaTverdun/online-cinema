using onlineCinema.Application.DTOs;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Application.Mapping;

public class CastMemberMapping
{
    public CastMemberDto MapToDto(CastMember entity)
    {
        return new CastMemberDto
        {
            CastId = entity.CastId,
            CastFirstName = entity.CastFirstName,
            CastLastName = entity.CastLastName,
            CastMiddleName = entity.CastMiddleName
        };
    }

    public CastMember MapToEntity(CastMemberCreateUpdateDto dto)
    {
        return new CastMember
        {
            CastId = dto.CastId,
            CastFirstName = dto.CastFirstName,
            CastLastName = dto.CastLastName,
            CastMiddleName = dto.CastMiddleName
        };
    }

    public void UpdateEntityFromDto(CastMemberCreateUpdateDto dto, CastMember entity)
    {
        entity.CastFirstName = dto.CastFirstName;
        entity.CastLastName = dto.CastLastName;
        entity.CastMiddleName = dto.CastMiddleName;
    }
}