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

        public async Task<(IEnumerable<Ticket> Items, int TotalCount)> GetTicketsSeekAsync(
                int? lastId,
                int pageSize,
                string? email = null,
                string? movieTitle = null,
                DateTime? date = null)
        {
            var query = dbSet.AsNoTracking()
                .Include(t => t.Session).ThenInclude(s => s.Movie)
                .Include(t => t.Session).ThenInclude(s => s.Hall)
                .Include(t => t.Seat)
                .Include(t => t.Booking)
                .Where(t => t.Booking.Payment != null && t.Booking.Payment.Status == PaymentStatus.Completed);

            if (!string.IsNullOrWhiteSpace(email))
            {
                query = query.Where(t => t.Booking.EmailAddress.Contains(email));
            }

            if (!string.IsNullOrWhiteSpace(movieTitle))
            {
                query = query.Where(t => t.Session.Movie.Title.Contains(movieTitle));
            }

            if (date.HasValue)
            {
                query = query.Where(t => t.Session.ShowingDateTime.Date == date.Value.Date);
            }

            int totalCount = await query.CountAsync();

            if (lastId.HasValue && lastId > 0)
            {
                query = query.Where(t => t.TicketId < lastId);
            }

            var items = await query
                .OrderByDescending(t => t.TicketId)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }
    }
}
