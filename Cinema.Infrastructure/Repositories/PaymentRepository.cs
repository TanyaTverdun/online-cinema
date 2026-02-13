using onlineCinema.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using onlineCinema.Domain.Entities;
using onlineCinema.Infrastructure.Data;

namespace onlineCinema.Infrastructure.Repositories
{
    public class PaymentRepository 
        : GenericRepository<Payment>, IPaymentRepository
    {
        private readonly ApplicationDbContext _db;

        public PaymentRepository(ApplicationDbContext db) 
            : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Payment>> GetAllWithBookingAsync()
        {
            return await _db.Payments
                .Include(p => p.Booking)
                .ToListAsync();
        }

        public async Task<Payment?> GetByIdWithBookingAsync(int id)
        {
            return await _db.Payments
                .Include(p => p.Booking)
                .FirstOrDefaultAsync(p => p.PaymentId == id);
        }

        public async Task<(IEnumerable<Payment> Items, int TotalCount)> 
            GetPaymentsSeekAsync(
                int? lastId, 
                int pageSize, 
                string? email, 
                string? movieTitle, 
                DateTime? date)
        {
            var baseQuery = dbSet.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(email))
            {
                baseQuery = baseQuery
                    .Where(p => p.Booking.EmailAddress.Contains(email));
            }

            if (date.HasValue)
            {
                baseQuery = baseQuery
                    .Where(p => p.PaymentDate.Date == date.Value.Date);
            }

            if (!string.IsNullOrWhiteSpace(movieTitle))
            {
                baseQuery = baseQuery.Where(
                    p => p.Booking.Tickets
                    .Any(t => t.Session.Movie.Title.Contains(movieTitle)));
            }

            int totalCount = await baseQuery.CountAsync();

            IQueryable<Payment> dataQuery = baseQuery
                .Include(p => p.Booking)
                    .ThenInclude(b => b.Tickets)
                        .ThenInclude(t => t.Session)
                            .ThenInclude(s => s.Movie)
                .Include(p => p.Booking)
                    .ThenInclude(b => b.Tickets)
                        .ThenInclude(t => t.Seat)
                .Include(p => p.Booking)
                    .ThenInclude(b => b.SnackBookings)
                        .ThenInclude(sb => sb.Snack);

            if (lastId.HasValue && lastId > 0)
            {
                dataQuery = dataQuery.Where(p => p.PaymentId < lastId);
            }

            var items = await dataQuery
                .OrderByDescending(p => p.PaymentId)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }
    }
}

