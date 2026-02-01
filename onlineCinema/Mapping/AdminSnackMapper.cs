using onlineCinema.Application.DTOs;
using onlineCinema.Areas.Admin.Models;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Mapping
{
    [Mapper]
    public partial class AdminSnackMapper
    {
        public partial SnackViewModel ToViewModel(SnackDto dto);
        public partial List<SnackViewModel> ToViewModelList(IEnumerable<SnackDto> dtos);
        public partial SnackDto ToDto(SnackViewModel model);
    }
}