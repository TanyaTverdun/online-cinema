using onlineCinema.Application.DTOs;
using onlineCinema.Domain.Entities;
using Riok.Mapperly.Abstractions;
using System.Collections.Generic;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class CastMemberMapping
    {
        public partial CastMemberDto MapToDto(Dancer castMember);

        public partial List<CastMemberDto> 
            MapToDtoList(IEnumerable<Dancer> castMembers);

        public partial Dancer MapToEntity(CastMemberCreateUpdateDto dto);

        public partial void UpdateEntityFromDto(
            CastMemberCreateUpdateDto dto, 
            Dancer entity);
    }
}