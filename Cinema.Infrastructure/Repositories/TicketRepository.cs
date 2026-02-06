using Microsoft.EntityFrameworkCore;
using onlineCinema.Application.Interfaces;
using onlineCinema.Domain.Entities;
using onlineCinema.Domain.Enums;
using onlineCinema.Infrastructure.Data;

namespace onlineCinema.Infrastructure.Repositories
{
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        private readonly ApplicationDbContext _db;

        public TicketRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Ticket>> GetBookedTicketsBySessionIdAsync(int sessionId)
        {
            return await _db.Tickets
                .Where(t => t.SessionId == sessionId)
                .Include(t => t.Seat)
                .ToListAsync();
        }
    }
}
