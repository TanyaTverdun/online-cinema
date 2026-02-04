using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Application.DTOs.AdminStatistics;

namespace onlineCinema.Application.Interfaces
{
    public interface IStatisticsRepository
    {
        Task<int> GetTotalTicketsSoldAsync();
        Task<decimal> GetTotalRevenueAsync();
        Task<List<TopItemDto>> GetTopSnacksAsync(int count);
        Task<List<TopItemDto>> GetTopMoviesAsync(int count);
        Task<List<DailyRevenueDto>> GetRevenueForLastDaysAsync(int days);
    }
}
