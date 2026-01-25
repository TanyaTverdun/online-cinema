using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Application.Interfaces
{
    public interface ISeatRepository : IGenericRepository<Seat>
    {
        Task<IEnumerable<Seat>> GetSeatsByHallIdAsync(int hallId);
    }
}
