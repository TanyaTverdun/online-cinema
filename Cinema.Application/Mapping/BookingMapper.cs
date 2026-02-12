using onlineCinema.Application.DTOs;
using Riok.Mapperly.Abstractions;
using onlineCinema.Domain.Entities;
using onlineCinema.Domain.Enums;

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

        [MapProperty(nameof(Ticket.TicketId), nameof(TicketInfoDto.TicketId))]
        [MapProperty(nameof(Ticket.Price), nameof(TicketInfoDto.Price))]
        [MapProperty(nameof(Ticket.Seat.RowNumber), nameof(TicketInfoDto.RowNumber))]
        [MapProperty(nameof(Ticket.Seat.SeatNumber), nameof(TicketInfoDto.SeatNumber))]
        [MapperIgnoreSource(nameof(Ticket.SessionId))]
        [MapperIgnoreSource(nameof(Ticket.SeatId))]
        [MapperIgnoreSource(nameof(Ticket.BookingId))]
        [MapperIgnoreSource(nameof(Ticket.Session))]
        [MapperIgnoreSource(nameof(Ticket.Seat.HallId))]
        [MapperIgnoreSource(nameof(Ticket.Seat.Coefficient))]
        [MapperIgnoreSource(nameof(Ticket.Seat.Hall))]
        [MapperIgnoreSource(nameof(Ticket.Seat.Tickets))]
        [MapperIgnoreSource(nameof(Ticket.Booking))]
        public partial TicketInfoDto ToTicketInfoDto(Ticket ticket);

        public BookingHistoryDto ToBookingHistoryDto(Booking booking)
        {
            var firstTicket = booking.Tickets.FirstOrDefault();
            var session = firstTicket?.Session;
            var movie = session?.Movie;
            var hall = session?.Hall;

            // Логіка для кнопки: 
            // 1. Оплачено (Completed)
            // 2. Час до сеансу > 60 хвилин
            bool isPaid = booking.Payment?.Status == PaymentStatus.Completed;
            bool isTimeValid = session != null && (session.ShowingDateTime - DateTime.Now).TotalMinutes > 60;
            bool notRefunded = booking.Payment?.Status != PaymentStatus.Refunded;

            return new BookingHistoryDto
            {
                BookingId = booking.BookingId,
                CreatedDateTime = booking.CreatedDateTime,
                TotalAmount = MapTotalAmount(booking),
                MovieTitle = movie?.Title ?? "Невідомо",
                SessionDateTime = session?.ShowingDateTime ?? DateTime.MinValue,
                MoviePoster = MapMoviePoster(movie?.PosterImage),
                HallName = MapHallName(hall?.HallNumber),
                PaymentStatus = MapPaymentStatus(booking.Payment?.Status),
                Tickets = booking.Tickets.Select(t => ToTicketInfoDtoWithSeatType(t)).ToList(),

                Snacks = booking.SnackBookings.Select(sb => new SnackInfoDto
                {
                    Name = sb.Snack.SnackName,
                    Quantity = sb.Quantity,
                    Price = sb.Snack.Price // Ціна за одиницю
                }).ToList(),

                CanRefund = isPaid && isTimeValid && notRefunded
            };
        }

        public PagedResultDto<BookingHistoryDto> MapToPagedResult(
            List<BookingHistoryDto> items,
            int totalCount,
            int pageSize,
            bool hasNext,
            bool hasPrevious)
        {
            return new PagedResultDto<BookingHistoryDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageSize = pageSize,

                LastId = items.LastOrDefault()?.BookingId,
                FirstId = items.FirstOrDefault()?.BookingId,

                HasNextPage = hasNext,
                HasPreviousPage = hasPrevious
            };
        }

        private TicketInfoDto ToTicketInfoDtoWithSeatType(Ticket ticket)
        {
            var dto = ToTicketInfoDto(ticket);
            dto.SeatType = MapSeatType(ticket.Seat.Type);
            return dto;
        }

        private string MapPaymentStatus(PaymentStatus? status)
        {
            return status switch
            {
                PaymentStatus.Pending => "Очікується",
                PaymentStatus.Completed => "Оплачено",
                PaymentStatus.Failed => "Помилка",
                PaymentStatus.Refunded => "Повернено",
                _ => status != null ? "Невідомо" : "Не оплачено"
            };
        }

        private decimal MapTotalAmount(Booking booking)
        {
            return booking.Payment?.Amount ?? booking.Tickets.Sum(t => t.Price);
        }

        private byte[]? MapMoviePoster(string? posterImage)
        {
            if (string.IsNullOrEmpty(posterImage))
            {
                return null;
            }

            return null;
        }

        private string MapHallName(byte? hallNumber)
        {
            return $"Зал {hallNumber ?? 0}";
        }

        private string MapSeatType(SeatType seatType)
        {
            return seatType == SeatType.VIP ? "VIP" : "Стандарт";
        }
    }
}