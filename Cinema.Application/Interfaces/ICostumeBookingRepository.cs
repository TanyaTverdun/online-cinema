using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Application.Interfaces
{
    public interface ICostumeBookingRepository : IGenericRepository<CostumeBooking>
    {
        Task<CostumeBooking?> GetByIdWithDetailsAsync(int id);
        Task UpdateWithDetailsAsync(CostumeBooking booking);
        Task<IEnumerable<CostumeBooking>> GetUserBookingsWithDetailsAsync(string userId);
        Task<(
            IEnumerable<CostumeBooking> Items, int TotalCount, bool HasNext, bool HasPrevious)>
            GetUserBookingsSeekAsync(
            string memberId, 
            int? lastId, 
            int? firstId, 
            int pageSize);
    }
}