using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Application.DTOs.AdminStatistics;
using onlineCinema.Domain.Entities;
using onlineCinema.Domain.Enums;
using Riok.Mapperly.Abstractions;

namespace onlineCinema.Application.Mapping
{
    [Mapper]
    public partial class StatisticsMapping
    {
        public AdminStatisticsDto CreateSummaryDto(
            int totalTickets,
            decimal totalRevenue,
            List<TopItemDto> topSnacks,
            List<TopItemDto> topMovies,
            List<DailyRevenueDto> dailyRevenue)
        {
            return new AdminStatisticsDto
            {
                TotalTicketsSold = totalTickets,
                TotalRevenue = totalRevenue,
                TopSnacks = topSnacks,
                TopMovies = topMovies,
                RevenueByDay = dailyRevenue
            };
        }
    }
}
