using onlineCinema.Application.DTOs;
using onlineCinema.ViewModels;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Mapping
{
    [Mapper]
    public partial class SnackViewModelMapper
    {
        public partial SnackItemViewModel MapSnackDtoToViewModel(
            SnackDto dto);

        private partial List<SnackItemViewModel> MapSnackDtoToViewModelList(
            IEnumerable<SnackDto> dtos);
        public SnackSelectionViewModel MapToSelectionViewModel(
            IEnumerable<SnackDto> snacks,
            int bookingId, 
            decimal seatsTotalPrice,
            DateTime lockUntil)
        {
            return new SnackSelectionViewModel
            {
                BookingId = bookingId,
                SeatsTotalPrice = seatsTotalPrice,
                AvailableSnacks = MapSnackDtoToViewModelList(snacks),
                LockUntil = lockUntil
            };
        }

        public partial SelectedSnackDto MapSnackItemViewModelToSelectedDto(
            SnackItemViewModel item);

        public partial List<SelectedSnackDto> 
            MapSnackItemViewModelToSelectedDtoList(
            List<SnackItemViewModel> items);
    }
}
