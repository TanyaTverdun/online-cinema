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
        Task<List<TopItemDto>> GetMoviesByPopularityAsync(int count, bool ascending);
        Task<List<TopItemDto>> GetSnacksByPopularityAsync(int count, bool ascending);
        Task<List<MovieOccupancyDto>> GetAverageOccupancyPerMovieAsync(int count);
    }
}
