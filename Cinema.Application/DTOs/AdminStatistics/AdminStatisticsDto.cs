using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Application.DTOs.AdminStatistics
{
    public class AdminStatisticsDto
    {
        public List<MovieOccupancyDto> MovieOccupancy { get; set; } = new();
        public List<TopItemDto> MostPopularMovies { get; set; } = new();
        public List<TopItemDto> LeastPopularMovies { get; set; } = new();
        public List<TopItemDto> MostPopularSnacks { get; set; } = new();
        public List<TopItemDto> LeastPopularSnacks { get; set; } = new();
    }
}
