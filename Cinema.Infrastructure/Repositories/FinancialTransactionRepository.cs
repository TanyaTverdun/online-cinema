using onlineCinema.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using onlineCinema.Domain.Entities;
using onlineCinema.Infrastructure.Data;

namespace onlineCinema.Infrastructure.Repositories
{
    public class FinancialTransactionRepository
     : GenericRepository<FinancialTransaction>, IFinancialTransactionRepository
    {
        private readonly ApplicationDbContext _db;

        public FinancialTransactionRepository(ApplicationDbContext db) 
            : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<FinancialTransaction>> GetAllWithBookingAsync()
        {
            return await _db.FinancialTransactions
                .Include(p => p.CostumeBooking)
                .ToListAsync();
        }

        public async Task<FinancialTransaction?> GetByIdWithBookingAsync(int id)
        {
            return await _db.FinancialTransactions
                .Include(p => p.CostumeBooking)
                .FirstOrDefaultAsync(p => p.PaymentId == id);
        }

        public async Task<(IEnumerable<FinancialTransaction> Items, int TotalCount)> 
            GetPaymentsSeekAsync(
                int? lastId, 
                int pageSize, 
                string? email, 
                string? performanceTitle, 
                DateTime? date)
        {
            var baseQuery = dbSet.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(email))
            {
                baseQuery = baseQuery
                    .Where(p => p.CostumeBooking.ApplicationUser.Email.Contains(email));
            }

            if (date.HasValue)
            {
                baseQuery = baseQuery
                    .Where(p => p.PaymentDate.Date == date.Value.Date);
            }

            if (!string.IsNullOrWhiteSpace(performanceTitle))
            {
                baseQuery = baseQuery.Where(
                    p => p.CostumeBooking.AttendanceLogs
                    .Any(t => t.DanceClass.Performance.Title.Contains(performanceTitle)));
            }

            int totalCount = await baseQuery.CountAsync();

            IQueryable<FinancialTransaction> dataQuery = baseQuery
                .Include(p => p.CostumeBooking)
                    .ThenInclude(b => b.AttendanceLogs)
                        .ThenInclude(t => t.DanceClass)
                            .ThenInclude(s => s.Performance)
                .Include(p => p.CostumeBooking)
                    .ThenInclude(b => b.AttendanceLogs)
                        .ThenInclude(t => t.Inventary)
                .Include(p => p.CostumeBooking)
                    .ThenInclude(b => b.MerchOrders)
                        .ThenInclude(sb => sb.StudioMerch);

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

