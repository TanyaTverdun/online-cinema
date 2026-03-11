using onlineCinema.Application.DTOs.AdminStatistics;
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
