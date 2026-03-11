using onlineCinema.Application.DTOs.Booking;
using onlineCinema.Application.DTOs.Common;
using onlineCinema.Application.DTOs.Session;
using onlineCinema.Application.DTOs.Snack;

namespace onlineCinema.Application.Services.Interfaces
{
    public interface IBookingService
    {
        Task<SessionSeatMapDto> GetSessionSeatMapAsync(int sessionId);
        Task AddSnacksToBookingAsync(
            int bookingId,
            List<SelectedSnackDto> selectedSnacks);
        Task<int> CreateBookingAsync(CreateBookingDto bookingDto);
        Task<decimal> GetTicketsPriceTotalAsync(int bookingId);
        Task<DateTime> GetBookingLockUntilAsync(int bookingId);
        Task CompletePaymentAsync(int bookingId);
        Task<IEnumerable<BookingHistoryDto>> GetBookingHistoryAsync(string userId);
        Task CancelBookingAsync(int bookingId, string userId);
        Task<PagedResultDto<BookingHistoryDto>> GetBookingHistorySeekAsync(
            string userId,
            int? lastId,
            int? firstId);
    }
}
