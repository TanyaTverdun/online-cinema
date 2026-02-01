using System.Collections.Generic;
using onlineCinema.Application.DTOs;
using onlineCinema.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class CastMemberMapping
    {
        [MapperIgnoreSource(nameof(CastMember.MovieCasts))]
        public partial CastMemberDto MapToDto(CastMember entity);

        [MapperIgnoreTarget(nameof(CastMember.MovieCasts))]
        public partial CastMember MapToEntity(CastMemberCreateUpdateDto dto);

        [MapperIgnoreTarget(nameof(CastMember.MovieCasts))]
        public partial void UpdateEntityFromDto(CastMemberCreateUpdateDto dto, CastMember entity);

        public partial IEnumerable<CastMemberDto> MapToDtoList(IEnumerable<CastMember> entities);
    }
}