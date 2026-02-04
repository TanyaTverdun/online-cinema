using Microsoft.EntityFrameworkCore;
using onlineCinema.Application.DTOs.AdminStatistics;
using onlineCinema.Application.Interfaces;
using onlineCinema.Domain.Entities;
using onlineCinema.Domain.Enums;
using onlineCinema.Infrastructure.Data;

namespace onlineCinema.Infrastructure.Repositories
{
    public class StatisticsRepository : GenericRepository<Payment>, IStatisticsRepository
    {
        private readonly ApplicationDbContext _db;

        public StatisticsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<int> GetTotalTicketsSoldAsync()
        {
            return await _db.Tickets
                .CountAsync(t => t.Booking.Payment != null
                    && t.Booking.Payment.Status == PaymentStatus.Completed);
        }

        public async Task<decimal> GetTotalRevenueAsync()
        {
            return await dbSet
                .Where(p => p.Status == PaymentStatus.Completed)
                .SumAsync(p => p.Amount);
        }

        public async Task<List<TopItemDto>> GetTopSnacksAsync(int count)
        {
            return await _db.Snacks
                .Select(s => new TopItemDto
                {
                    Name = s.SnackName,
                    Revenue = s.Price * (decimal)s.SnackBookings
                        .Where(sb => sb.Booking.Payment != null && sb.Booking.Payment.Status == PaymentStatus.Completed)
                        .Sum(sb => (int)sb.Quantity),
                    Count = s.SnackBookings
                        .Where(sb => sb.Booking.Payment != null && sb.Booking.Payment.Status == PaymentStatus.Completed)
                        .Sum(sb => (int)sb.Quantity)
                })
                .OrderByDescending(x => x.Revenue)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<TopItemDto>> GetTopMoviesAsync(int count)
        {
            return await _db.Movies
                .Select(m => new TopItemDto
                {
                    Name = m.Title,
                    Revenue = m.Sessions
                        .SelectMany(s => s.Tickets)
                        .Where(t => t.Booking.Payment != null && t.Booking.Payment.Status == PaymentStatus.Completed)
                        .Sum(t => t.Price),
                    Count = m.Sessions
                        .SelectMany(s => s.Tickets)
                        .Count(t => t.Booking.Payment != null && t.Booking.Payment.Status == PaymentStatus.Completed)
                })
                .OrderByDescending(x => x.Revenue)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<DailyRevenueDto>> GetRevenueForLastDaysAsync(int days)
        {
            var startDate = DateTime.Now.Date.AddDays(-days);

            return await dbSet
                .Where(p => p.Status == PaymentStatus.Completed && p.PaymentDate >= startDate)
                .GroupBy(p => p.PaymentDate.Date)
                .Select(g => new DailyRevenueDto
                {
                    Date = g.Key,
                    Revenue = g.Sum(p => p.Amount)
                })
                .OrderBy(x => x.Date)
                .ToListAsync();
        }
    }
}