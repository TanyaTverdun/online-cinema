using onlineCinema.Application.DTOs;
using onlineCinema.Domain.Entities;
using Riok.Mapperly.Abstractions;
using System.Collections.Generic;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class CastMemberMapping
    {
        public partial CastMemberDto MapToDto(CastMember castMember);

        public partial List<CastMemberDto> MapToDtoList(IEnumerable<CastMember> castMembers);

        public partial CastMember MapToEntity(CastMemberCreateUpdateDto dto);

        public partial void UpdateEntityFromDto(CastMemberCreateUpdateDto dto, CastMember entity);
    }
}