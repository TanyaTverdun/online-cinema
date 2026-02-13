using onlineCinema.Application.DTOs;
using onlineCinema.Areas.Admin.Models;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Mapping
{
    [Mapper]
    public partial class AdminCastMemberMapper
    {
        public partial CastMemberViewModel ToViewModel(CastMemberDto dto);
        public partial List<CastMemberViewModel> ToViewModelList(
            IEnumerable<CastMemberDto> dtos);

        public partial CastMemberCreateUpdateDto ToCreateUpdateDto(
            CastMemberViewModel model);
    }
}