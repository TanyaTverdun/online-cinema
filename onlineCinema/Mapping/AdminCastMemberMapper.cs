using onlineCinema.Application.DTOs;
using onlineCinema.Areas.Admin.Models;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Mapping
{
    [Mapper]
    public partial class AdminDancerMapper
    {
        public partial DancerViewModel ToViewModel(DancerDto dto);
        public partial List<DancerViewModel> ToViewModelList(
            IEnumerable<DancerDto> dtos);

        public partial DancerCreateUpdateDto ToCreateUpdateDto(
         DancerViewModel model);
    }
}