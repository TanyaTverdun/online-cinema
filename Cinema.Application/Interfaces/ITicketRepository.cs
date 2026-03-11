using onlineCinema.Domain.Entities;

namespace onlineCinema.Application.Interfaces
{
    public interface ITicketRepository : IGenericRepository<Ticket>
    {
        Task<IEnumerable<Ticket>> GetBookedTicketsBySessionIdAsync(int sessionId);
    }
}