using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Application.DTOs;

namespace onlineCinema.Application.Services.Interfaces
{
    public interface IBookingService
    {
        Task<SessionSeatMapDto> GetSessionSeatMapAsync(int sessionId);
        Task AddSnacksToBookingAsync(int bookingId, List<SelectedSnackDto> selectedSnacks);
        Task<int> CreateBookingAsync(CreateBookingDto bookingDto);
        Task<decimal> GetTicketsPriceTotalAsync(int bookingId);
    }
}
