using onlineCinema.Application.DTOs;
using onlineCinema.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class BookingMapper
    {
        [MapProperty(nameof(Seat.RowNumber), nameof(SeatDto.Row))]
        [MapProperty(nameof(Seat.SeatNumber), nameof(SeatDto.Number))]
        private partial SeatDto MapToSeatBase(Seat seat);

        public SeatDto MapToSeatDto(Seat seat, decimal basePrice, HashSet<int> bookedSeatIds)
        {
            var dto = MapToSeatBase(seat);

            dto.Price = basePrice * (decimal)seat.Coefficient;
            dto.IsBooked = bookedSeatIds.Contains(seat.SeatId);

            return dto;
        }

        [MapProperty(nameof(Session.Hall.HallNumber), nameof(SessionSeatMapDto.HallNumber))]
        [MapProperty(nameof(Session.Movie.Title), nameof(SessionSeatMapDto.MovieTitle))]
        [MapProperty(nameof(Session.ShowingDateTime), nameof(SessionSeatMapDto.ShowingDate))]
        private partial SessionSeatMapDto MapToSessionBase(Session session);

        public SessionSeatMapDto MapToSessionSeatMapDto(Session session, List<SeatDto> seatDtos)
        {
            var dto = MapToSessionBase(session);
            
            dto.Seats = seatDtos;

            return dto;
        }

        [MapProperty(nameof(CreateBookingDto.UserEmail), nameof(Booking.EmailAddress))]
        [MapProperty(nameof(CreateBookingDto.UserId), nameof(Booking.ApplicationUserId))]
        private partial Booking MapToBookingBase(CreateBookingDto dto);

        public Booking MapCreateBookingDtoToEntity(CreateBookingDto dto)
        {
            var booking = MapToBookingBase(dto);

            booking.CreatedDateTime = DateTime.Now;

            booking.Tickets = new List<Ticket>();

            return booking;
        }

        public Ticket MapToTicket(Seat seat, int sessionId, decimal basePrice, DateTime lockUntil)
        {
            return new Ticket
            {
                SessionId = sessionId,
                SeatId = seat.SeatId,
                Price = basePrice * (decimal)seat.Coefficient,
                LockUntil = lockUntil
            };
        }
    }
}
