using onlineCinema.Application.DTOs.AdminTickets;
using onlineCinema.Domain.Entities;
using onlineCinema.Domain.Enums;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class PaymentMapper
    {
        public FinancialTransaction CreateCompletedPayment(
            int bookingId, 
            decimal amount)
        {
            return new FinancialTransaction
            {
                BookingId = bookingId,
                Amount = amount,
                PaymentDate = DateTime.Now,
                Status = PaymentStatus.Completed
            };
        }

        [MapProperty("Booking.EmailAddress", "UserEmail")]
        public partial PaymentAdminDto MapToDto(FinancialTransaction payment);

        public List<PaymentAdminDto> 
            MapToDtoList(IEnumerable<FinancialTransaction> payments)
        {
            return payments.Select(p => {
                var dto = MapToDto(p);
                var booking = p.Booking;
                var firstTicket = booking?.Tickets.FirstOrDefault();

                dto.OrderDate = booking?.CreatedDateTime ?? p.PaymentDate;

                dto.MovieTitle = 
                    firstTicket?.Session?.Movie?.Title ?? "Без фільму";

                dto.MovieSessionDateTime = 
                    firstTicket?.Session?.ShowingDateTime ?? DateTime.MinValue;

                dto.TicketCount = booking?.Tickets.Count ?? 0;

                dto.TicketsInfo = booking?.Tickets
                    .Select(t => $"Ряд {t.ItemId.RowNumber}, Місце {t.ItemId.SeatNumber} " +
                        $"({(t.ItemId.Type == ConditionStatus.VIP ? "VIP" : "Стандарт")})")
                    .ToList() ?? new();

                dto.SnacksInfo = booking?.SnackBookings
                    .Select(sb => $"{sb.Snack.SnackName} x{sb.Quantity}")
                    .ToList() ?? new();

                return dto;
            }).ToList();
        }

        public PagedResult<PaymentAdminDto> MapToPagedResult(
            IEnumerable<FinancialTransaction> entities, int totalCount, int pageSize)
        {
            var dtos = MapToDtoList(entities);

            return new PagedResult<PaymentAdminDto>
            {
                Items = dtos,
                TotalCount = totalCount,
                PageSize = pageSize,
                HasNextPage = dtos.Count == pageSize
            };
        }
    }
}
