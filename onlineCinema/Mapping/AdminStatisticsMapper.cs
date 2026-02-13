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
                OccupancyLabels =
                dto.MovieOccupancy.Select(x => x.MovieTitle).ToList(),
                OccupancyData = 
                dto.MovieOccupancy
                .Select(x => x.OccupancyPercentage)
                .ToList(),

                PopularMoviesLabels = 
                dto.MostPopularMovies.Select(x => x.Name).ToList(),
                PopularMoviesData = 
                dto.MostPopularMovies.Select(x => x.Count).ToList(),

                LeastMoviesLabels = 
                dto.LeastPopularMovies.Select(x => x.Name).ToList(),
                LeastMoviesData = 
                dto.LeastPopularMovies.Select(x => x.Count).ToList(),

                LeastSnacksLabels =
                dto.LeastPopularSnacks.Select(x => x.Name).ToList(),
                LeastSnacksData = 
                dto.LeastPopularSnacks.Select(x => x.Count).ToList()
            };
        }
    }
}
