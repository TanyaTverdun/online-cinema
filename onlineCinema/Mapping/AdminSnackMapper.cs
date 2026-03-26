using onlineCinema.Application.DTOs.Snack;
using onlineCinema.Areas.Admin.Models;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Mapping
{
    [Mapper]
    public partial class AdminSnackMapper
    {
        [MapProperty(nameof(SnackDto.Name), nameof(SnackViewModel.SnackName))]
        public partial SnackViewModel ToViewModel(SnackDto dto);

        public List<SnackViewModel> ToViewModelList(IEnumerable<SnackDto> dtos)
            => dtos.Select(ToViewModel).ToList();

        [MapProperty(nameof(SnackViewModel.SnackName), nameof(SnackDto.Name))]
        public partial SnackDto ToDto(SnackViewModel model);
    }
}