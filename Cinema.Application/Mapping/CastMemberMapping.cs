using onlineCinema.Application.DTOs.Person;
using onlineCinema.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class CastMemberMapping
    {
        public partial CastMemberDto MapToDto(CastMember castMember);

        public partial List<CastMemberDto>
            MapToDtoList(IEnumerable<CastMember> castMembers);

        public partial CastMember MapToEntity(CastMemberDto dto);

        public partial void UpdateEntityFromDto(
            CastMemberDto dto,
            CastMember entity);
    }
}