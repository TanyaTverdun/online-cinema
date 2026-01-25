using onlineCinema.Application.DTOs;

namespace onlineCinema.Application.Services.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingHistoryDto>> GetBookingHistoryAsync(string userId);
    }
}
