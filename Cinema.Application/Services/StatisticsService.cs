using Microsoft.Extensions.Options;
using onlineCinema.Application.Configurations;
using onlineCinema.Application.DTOs.AdminStatistics;
using onlineCinema.Application.Interfaces;
using onlineCinema.Application.Mapping;
using onlineCinema.Application.Services.Interfaces;

namespace onlineCinema.Application.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly StatisticsMapping _mapper;
        private readonly StatisticsSettings _settings;

        public StatisticsService(
            IUnitOfWork unitOfWork,
            StatisticsMapping mapper,
            IOptions<StatisticsSettings> settings)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _settings = settings.Value;
        }

        public async Task<AdminStatisticsDto> GetAdminStatisticsAsync()
        {
            var occupancy = await _unitOfWork.Statistics
                .GetAverageOccupancyPerMovieAsync(_settings.OccupancyCount);

            var popMovies = await _unitOfWork.Statistics
                .GetMoviesByPopularityAsync(_settings.TopMoviesCount, ascending: false);
            var leastMovies = await _unitOfWork.Statistics
                .GetMoviesByPopularityAsync(_settings.TopMoviesCount, ascending: true);

            var popSnacks = await _unitOfWork.Statistics
                .GetSnacksByPopularityAsync(_settings.TopSnacksCount, ascending: false);
            var leastSnacks = await _unitOfWork.Statistics
                .GetSnacksByPopularityAsync(_settings.TopSnacksCount, ascending: true);

            return _mapper.CreateSummaryDto(
                occupancy,
                popMovies,
                leastMovies,
                popSnacks,
                leastSnacks
            );
        }
    }
}