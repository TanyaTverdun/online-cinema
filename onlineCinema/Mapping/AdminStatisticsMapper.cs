using onlineCinema.Application.DTOs.AdminStatistics;
using onlineCinema.Areas.Admin.Models;

namespace onlineCinema.Mapping
{
    public class AdminStatisticsMapper
    {
        public AdminStatisticsViewModel MapToViewModel(AdminStatisticsDto dto)
        {
            return new AdminStatisticsViewModel
            {
                TotalTicketsSold = dto.TotalTicketsSold,
                TotalRevenue = dto.TotalRevenue,

                SnackLabels = dto.TopSnacks.Select(s => s.Name).ToList(),
                SnackRevenueData = dto.TopSnacks.Select(s => s.Revenue).ToList(),

                MovieLabels = dto.TopMovies.Select(m => m.Name).ToList(),
                MovieRevenueData = dto.TopMovies.Select(m => m.Revenue).ToList(),

                DaysLabels = dto.RevenueByDay.Select(d => d.Date.ToString("dd.MM")).ToList(),
                DailyRevenueData = dto.RevenueByDay.Select(d => d.Revenue).ToList()
            };
        }
    }
}
