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
            List<MovieOccupancyDto> movieOccupancy,
            List<TopItemDto> mostPopularMovies,
            List<TopItemDto> leastPopularMovies,
            List<TopItemDto> mostPopularSnacks,
            List<TopItemDto> leastPopularSnacks)
        {
            return new AdminStatisticsDto
            {
                MovieOccupancy = movieOccupancy,
                MostPopularMovies = mostPopularMovies,
                LeastPopularMovies = leastPopularMovies,
                MostPopularSnacks = mostPopularSnacks,
                LeastPopularSnacks = leastPopularSnacks
            };
        }
    }
}
