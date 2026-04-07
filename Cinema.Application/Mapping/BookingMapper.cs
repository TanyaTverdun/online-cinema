using onlineCinema.Application.DTOs;
using Riok.Mapperly.Abstractions;
using onlineCinema.Domain.Entities;
using onlineCinema.Domain.Enums;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class BookingMapper
    {
        [MapProperty(nameof(Inventary.RowNumber), nameof(SeatDto.Row))]
        [MapProperty(nameof(Inventary.SeatNumber), nameof(SeatDto.Number))]
        private partial SeatDto MapToSeatBase(Inventary seat);

        public SeatDto MapToSeatDto(
            Inventary seat, 
            decimal basePrice, 
            HashSet<int> bookedSeatIds)
        {
            var dto = MapToSeatBase(seat);

            dto.Price = basePrice * (decimal)seat.Coefficient;
            dto.IsBooked = bookedSeatIds.Contains(seat.SeatId);

            return dto;
        }

        [MapProperty(nameof(DanceClass.Hall.HallNumber), 
            nameof(DanceClassMapDto.HallNumber))]
        [MapProperty(nameof(DanceClass.Movie.Title), 
            nameof(DanceClassMapDto.MovieTitle))]
        [MapProperty(nameof(DanceClass.ShowingDateTime), 
            nameof(DanceClassMapDto.ShowingDate))]
        private partial DanceClassMapDto MapToSessionBase(DanceClass session);

        public DanceClassMapDto MapToSessionSeatMapDto(
            DanceClass session, 
            List<SeatDto> seatDtos)
        {
            var dto = MapToSessionBase(session);
            
            dto.Seats = seatDtos;

            return dto;
        }

        [MapProperty(nameof(CreateBookingDto.UserEmail), 
            nameof(CostumeBooking.EmailAddress))]
        [MapProperty(nameof(CreateBookingDto.UserId), 
            nameof(CostumeBooking.ApplicationUserId))]
        private partial CostumeBooking MapToBookingBase(CreateBookingDto dto);

        public CostumeBooking MapCreateBookingDtoToEntity(CreateBookingDto dto)
        {
            var booking = MapToBookingBase(dto);

            booking.CreatedDateTime = DateTime.Now;

            booking.Tickets = new List<AttendanceLog>();

            return booking;
        }

        public AttendanceLog MapToTicket(
            Inventary seat, 
            int sessionId, 
            decimal basePrice, 
            DateTime lockUntil)
        {
            return new AttendanceLog
            {
                SessionId = sessionId,
                SeatId = seat.SeatId,
                Price = basePrice * (decimal)seat.Coefficient,
                LockUntil = lockUntil
            };
        }

        [MapProperty(nameof(AttendanceLog.TicketId), nameof(AttendanceLogInfoDto.TicketId))]
        [MapProperty(nameof(AttendanceLog.Price), nameof(AttendanceLogInfoDto.Price))]
        [MapProperty(nameof(AttendanceLog.ItemId.RowNumber), nameof(AttendanceLogInfoDto.RowNumber))]
        [MapProperty(nameof(AttendanceLog.ItemId.SeatNumber), nameof(AttendanceLogInfoDto.SeatNumber))]
        [MapperIgnoreSource(nameof(AttendanceLog.SessionId))]
        [MapperIgnoreSource(nameof(AttendanceLog.SeatId))]
        [MapperIgnoreSource(nameof(AttendanceLog.BookingId))]
        [MapperIgnoreSource(nameof(AttendanceLog.Session))]
        [MapperIgnoreSource(nameof(AttendanceLog.ItemId.HallId))]
        [MapperIgnoreSource(nameof(AttendanceLog.ItemId.Coefficient))]
        [MapperIgnoreSource(nameof(AttendanceLog.ItemId.Hall))]
        [MapperIgnoreSource(nameof(AttendanceLog.ItemId.Tickets))]
        [MapperIgnoreSource(nameof(AttendanceLog.Booking))]
        public partial AttendanceLogInfoDto ToTicketInfoDto(AttendanceLog ticket);

        public BookingHistoryDto ToBookingHistoryDto(CostumeBooking booking)
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

        private AttendanceLogInfoDto ToTicketInfoDtoWithSeatType(AttendanceLog ticket)
        {
            var dto = ToTicketInfoDto(ticket);
            dto.SeatType = MapSeatType(ticket.ItemId.Type);
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

        private decimal MapTotalAmount(CostumeBooking booking)
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

        private string MapSeatType(ConditionStatus seatType)
        {
            return seatType == ConditionStatus.VIP ? "VIP" : "Стандарт";
        }
    }
}