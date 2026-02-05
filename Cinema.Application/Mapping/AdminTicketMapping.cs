using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Application.DTOs.AdminTickets;
using onlineCinema.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class AdminTicketMapping
    {
        [MapProperty("Session.Movie.Title", nameof(TicketAdminDto.MovieTitle))]
        [MapProperty("Session.ShowingDateTime", nameof(TicketAdminDto.ShowingDateTime))]
        [MapProperty("Session.Hall.HallNumber", nameof(TicketAdminDto.HallNumber))]
        [MapProperty("Seat.RowNumber", nameof(TicketAdminDto.Row))]
        [MapProperty("Seat.SeatNumber", nameof(TicketAdminDto.Number))]
        [MapProperty("Booking.EmailAddress", nameof(TicketAdminDto.UserEmail))]
        [MapProperty("Booking.CreatedDateTime", nameof(TicketAdminDto.PurchaseDate))]

        [MapperIgnoreSource(nameof(Ticket.SessionId))]
        [MapperIgnoreSource(nameof(Ticket.SeatId))]
        [MapperIgnoreSource(nameof(Ticket.BookingId))]
        [MapperIgnoreSource(nameof(Ticket.LockUntil))]
        [MapperIgnoreSource(nameof(Ticket.Session))]
        [MapperIgnoreSource(nameof(Ticket.Seat))]
        [MapperIgnoreSource(nameof(Ticket.Booking))]
        public partial TicketAdminDto MapToDto(Ticket ticket);

        public partial List<TicketAdminDto> MapToDtoList(IEnumerable<Ticket> tickets);

        public PagedResult<TicketAdminDto> MapToPagedResult(IEnumerable<Ticket> entities, int totalCount, int pageSize)
        {
            var dtos = MapToDtoList(entities);

            return new PagedResult<TicketAdminDto>
            {
                Items = dtos,
                TotalCount = totalCount,
                HasNextPage = dtos.Count == pageSize
            };
        }
    }
}
