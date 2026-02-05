using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Application.Interfaces
{
    public interface ITicketRepository : IGenericRepository<Ticket>
    {
        Task<(IEnumerable<Ticket> Items, int TotalCount)> GetTicketsSeekAsync(
                int? lastId,
                int pageSize,
                string? email = null,
                string? movieTitle = null,
                DateTime? date = null);
        Task<IEnumerable<Ticket>> GetBookedTicketsBySessionIdAsync(int sessionId);
    }
}