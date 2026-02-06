using onlineCinema.Application.DTOs;
using onlineCinema.Areas.Admin.Models;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Mapping
{
    [Mapper]
    public partial class SessionViewModelMapper
    {
        public partial SessionCreateDto MapToCreateDto(SessionCreateViewModel vm);

        [MapProperty(nameof(SessionEditViewModel.Id), nameof(SessionUpdateDto.Id))]
        public partial SessionUpdateDto MapToUpdateDto(SessionEditViewModel vm);

        [MapProperty(nameof(SessionDto.Id), nameof(SessionEditViewModel.Id))]
        public partial SessionEditViewModel MapToEditViewModel(SessionDto dto);
        public partial SessionListViewModel MapToListViewModel(SessionDto dto);
        public partial IEnumerable<SessionListViewModel> MapToListViewModelList(IEnumerable<SessionDto> dtos);
        [MapProperty(nameof(SessionDto.HallNumber), nameof(SessionDeleteViewModel.HallName), Use = nameof(MapHallNumberToString))]
        public partial SessionDeleteViewModel MapToDeleteViewModel(SessionDto dto);

        private string MapHallNumberToString(int hallNumber) => $"Зал {hallNumber}";
    }
}
  
