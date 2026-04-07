using Microsoft.EntityFrameworkCore;
using onlineCinema.Application.DTOs.AdminStatistics;
using onlineCinema.Application.Interfaces;
using onlineCinema.Domain.Entities;
using onlineCinema.Domain.Enums;
using onlineCinema.Infrastructure.Data;

namespace onlineCinema.Infrastructure.Repositories
{
    public class StatisticRepository
        : GenericRepository<FinancialTransaction>, IStatisticRepository
    {
        private readonly ApplicationDbContext _db;

        public StatisticRepository(ApplicationDbContext db)
            : base(db)
        {
            _db = db;
        }

        // 1. Популярність виступів (Performances) за кількістю оплачених відвідувань
        public async Task<List<TopItemDto>> GetPerformancesByPopularityAsync(int count, bool ascending)
        {
            var query = _db.Performances
                .Select(p => new TopItemDto
                {
                    Name = p.Title,
                    Count = p.DanceClassesS
                        .SelectMany(dc => dc.AttendanceLogs)
                        .Count(al => al.CostumeBooking.FinancialTransaction != null
                            && al.CostumeBooking.FinancialTransaction.Status == PaymentStatus.Completed),
                    Revenue = 0 
                });

            query = ascending ? query.OrderBy(x => x.Count)
                : query.OrderByDescending(x => x.Count);

            return await query.Take(count).ToListAsync();
        }

        // 2. Популярність мерчу (StudioMerch)
        public async Task<List<TopItemDto>> GetMerchByPopularityAsync(int count, bool ascending)
        {
            var query = _db.StudioMerches
                .Select(m => new TopItemDto
                {
                    Name = m.ProductName,
                    Count = m.MerchOrders
                        .Where(mo => mo.CostumeBooking.FinancialTransaction != null
                            && mo.CostumeBooking.FinancialTransaction.Status == PaymentStatus.Completed)
                        .Sum(mo => (int)mo.Quantity),
                    Revenue = 0
                });

            query = ascending ? query.OrderBy(x => x.Count)
                : query.OrderByDescending(x => x.Count);

            return await query.Take(count).ToListAsync();
        }

        // 3. Середня заповненість груп (Occupancy)
        public async Task<List<MovieOccupancyDto>> GetAverageOccupancyPerMovieAsync(int count)
        {
            var data = await _db.Performances
                .Select(p => new
                {
                    Title = p.Title,
                    Classes = p.DanceClassesS.Select(dc => new
                    {
                        TotalCapacity = dc.DancerHall.MaxPeople,
                        AttendedCount = dc.AttendanceLogs
                            .Count(al => al.CostumeBooking.FinancialTransaction != null
                                && al.CostumeBooking.FinancialTransaction.Status == PaymentStatus.Completed)
                    })
                })
                .ToListAsync();

            var result = data.Select(p => new MovieOccupancyDto
            {
                MovieTitle = p.Title, 
                OccupancyPercentage = p.Classes.Any()
                    ? Math.Round(p.Classes
                        .Average(c => c.TotalCapacity > 0 ?
                            (double)c.AttendedCount / c.TotalCapacity * 100 : 0), 1)
                    : 0
            })
            .OrderByDescending(x => x.OccupancyPercentage)
            .Take(count)
            .ToList();

            return result;
        }

       
        //public Task<List<TopItemDto>> GetMoviesByPopularityAsync(int count, bool ascending)
        //    => GetPerformancesByPopularityAsync(count, ascending);

        //public Task<List<TopItemDto>> GetSnacksByPopularityAsync(int count, bool ascending)
        //    => GetMerchByPopularityAsync(count, ascending);
    }
}