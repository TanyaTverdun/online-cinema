using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Application.Interfaces
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        Task<Booking?> GetByIdWithDetailsAsync(int id);
        Task UpdateWithDetailsAsync(Booking booking);
        Task<IEnumerable<Booking>> GetUserBookingsWithDetailsAsync(string userId);
    }
}