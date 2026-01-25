using onlineCinema.Application.DTOs;
using onlineCinema.Application.Interfaces;
using onlineCinema.Application.Mapping;
using onlineCinema.Application.Services.Interfaces;

namespace onlineCinema.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BookingMapper _bookingMapper;

        public BookingService(IUnitOfWork unitOfWork, BookingMapper bookingMapper)
        {
            _unitOfWork = unitOfWork;
            _bookingMapper = bookingMapper;
        }

        public async Task<IEnumerable<BookingHistoryDto>> GetBookingHistoryAsync(string userId)
        {
            var bookings = await _unitOfWork.Booking.GetUserBookingsWithDetailsAsync(userId);
            
            if (bookings == null)
            {
                throw new KeyNotFoundException($"Bookings not found for user with ID: {userId}");
            }

            return bookings.Select(booking => _bookingMapper.ToBookingHistoryDto(booking));
        }
    }
}
