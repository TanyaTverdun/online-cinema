using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Application.DTOs.AdminStatistics
{
    public class AdminStatisticsDto
    {
        public int TotalTicketsSold { get; set; }
        public decimal TotalRevenue { get; set; }

        public List<TopItemDto> TopSnacks { get; set; } = new();
        public List<TopItemDto> TopMovies { get; set; } = new();
        public List<DailyRevenueDto> RevenueByDay { get; set; } = new();
    }
}
