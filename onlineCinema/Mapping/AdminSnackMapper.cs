using onlineCinema.Application.DTOs;
using onlineCinema.Areas.Admin.Models;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Mapping
{
    [Mapper]
    public partial class AdminSnackMapper
    {
        [MapProperty(nameof(SnackDto.Name), nameof(SnackViewModel.SnackName))]
        public partial SnackViewModel ToViewModel(SnackDto dto);
        public partial List<SnackViewModel> ToViewModelList(IEnumerable<SnackDto> dtos);
        [MapProperty(nameof(SnackViewModel.SnackName), nameof(SnackDto.Name))]
        public partial SnackDto ToDto(SnackViewModel model);
    }
}