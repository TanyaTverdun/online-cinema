using onlineCinema.Application.DTOs;
using onlineCinema.Domain.Entities;
using onlineCinema.ViewModels;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Mapping
{
    [Mapper]
    public partial class BookingViewModelMapper
    {
        public partial SeatViewModel MapSeatToViewModel(SeatDto seatDto);

        private partial BookingViewModel MapSessionSeatMapToViewModelBase(SessionSeatMapDto dto);

        public BookingViewModel MapSessionSeatMapToViewModel(SessionSeatMapDto dto)
        {
            var vm = MapSessionSeatMapToViewModelBase(dto);

            vm.HallName = $"Зал {dto.HallNumber}";

            return vm;
        }

        [MapProperty(nameof(BookingInputViewModel.SelectedSeatIds), nameof(CreateBookingDto.SeatIds))]
        private partial CreateBookingDto MapBookingInputViewModeBase(BookingInputViewModel model);

        public CreateBookingDto MapBookingInputViewModelToDto(BookingInputViewModel model, ApplicationUser user)
        {
            var dto = MapBookingInputViewModeBase(model);

            dto.UserId = user.Id;
            dto.UserEmail = user.Email ?? string.Empty;
            dto.UserDateOfBirth = user.DateOfBirth;

            return dto;
        }
    }
}
