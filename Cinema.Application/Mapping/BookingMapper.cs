using Riok.Mapperly.Abstractions;
using onlineCinema.Application.DTOs;
using onlineCinema.Domain.Entities;
using onlineCinema.Domain.Enums;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class BookingMapper
    {
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
                Tickets = booking.Tickets.Select(t => ToTicketInfoDtoWithSeatType(t)).ToList()
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
            
            // If it's a URL or path, return null (can't convert to byte[] without fetching)
            // If it's base64, we could decode it, but for now return null
            // In a real scenario, you might want to fetch the image or store it differently
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

