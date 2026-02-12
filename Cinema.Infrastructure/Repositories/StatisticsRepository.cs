using Microsoft.EntityFrameworkCore;
using onlineCinema.Application.DTOs.AdminStatistics;
using onlineCinema.Application.Interfaces;
using onlineCinema.Domain.Entities;
using onlineCinema.Domain.Enums;
using onlineCinema.Infrastructure.Data;

namespace onlineCinema.Infrastructure.Repositories
{
    public class StatisticsRepository 
        : GenericRepository<Payment>, IStatisticsRepository
    {
        private readonly ApplicationDbContext _db;

        public StatisticsRepository(ApplicationDbContext db) 
            : base(db)
        {
            _db = db;
        }

        public async Task<List<TopItemDto>> 
            GetMoviesByPopularityAsync(int count, bool ascending)
        {
            var query = _db.Movies
                .Select(m => new TopItemDto
                {
                    Name = m.Title,
                    Count = m.Sessions
                        .SelectMany(s => s.Tickets)
                        .Count(t => t.Booking.Payment != null 
                            && t.Booking.Payment.Status == PaymentStatus.Completed),
                    Revenue = 0
                });

            query = ascending ? query.OrderBy(x => x.Count) 
                : query.OrderByDescending(x => x.Count); 

            return await query.Take(count).ToListAsync();
        }

        public async Task<List<TopItemDto>> GetSnacksByPopularityAsync(
            int count, 
            bool ascending)
        {
            var query = _db.Snacks
                .Select(s => new TopItemDto
                {
                    Name = s.SnackName,
                    Count = s.SnackBookings
                        .Where(sb => sb.Booking.Payment != null 
                            && sb.Booking.Payment.Status == PaymentStatus.Completed)
                        .Sum(sb => (int)sb.Quantity),
                    Revenue = 0
                });

            query = ascending ? query.OrderBy(x => x.Count) 
                : query.OrderByDescending(x => x.Count);

            return await query.Take(count).ToListAsync();
        }

        public async Task<List<MovieOccupancyDto>> 
            GetAverageOccupancyPerMovieAsync(int count)
        {
            var data = await _db.Movies
                .Select(m => new
                {
                    Title = m.Title,
                    Sessions = m.Sessions.Select(s => new
                    {
                        TotalSeats = s.Hall.Seats.Count,
                        SoldTickets = s.Tickets
                        .Count(t => t.Booking.Payment != null 
                            && t.Booking.Payment.Status == PaymentStatus.Completed)
                    })
                })
                .ToListAsync();

            var result = data.Select(m => new MovieOccupancyDto
            {
                MovieTitle = m.Title,
                OccupancyPercentage = m.Sessions.Any()
                    ? Math.Round(m.Sessions
                        .Average(s => s.TotalSeats > 0 ? 
                            (double)s.SoldTickets / s.TotalSeats * 100 : 0), 1)
                    : 0
            })
            .OrderByDescending(x => x.OccupancyPercentage)
            .Take(count)
            .ToList();

            return result;
        }
    }
}