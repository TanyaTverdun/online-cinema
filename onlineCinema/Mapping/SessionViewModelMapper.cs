using onlineCinema.Application.DTOs.Session;
using onlineCinema.Areas.Admin.Models;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Mapping
{
    [Mapper]
    public partial class SessionViewModelMapper
    {
        public partial SessionFormDto MapToDto(SessionViewModel vm);

        [MapProperty(nameof(SessionDto.Id),
            nameof(SessionViewModel.Id))]
        public partial SessionViewModel MapToEditViewModel(SessionDto dto);
        public partial SessionListViewModel MapToListViewModel(
            SessionDto dto);
        public partial IEnumerable<SessionListViewModel>
            MapToListViewModelList(IEnumerable<SessionDto> dtos);
        [MapProperty(nameof(SessionDto.HallNumber),
            nameof(SessionViewModel.HallName),
            Use = nameof(MapHallNumberToString))]
        public partial SessionViewModel MapToDeleteViewModel(SessionDto dto);

        private string MapHallNumberToString(int hallNumber) =>
            $"Зал {hallNumber}";
    }
}

