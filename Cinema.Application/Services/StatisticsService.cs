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
            var tickets = await _unitOfWork.Statistics.GetTotalTicketsSoldAsync();
            var revenue = await _unitOfWork.Statistics.GetTotalRevenueAsync();
            var snacks = await _unitOfWork.Statistics.GetTopSnacksAsync(_settings.TopSnacksCount);
            var movies = await _unitOfWork.Statistics.GetTopMoviesAsync(_settings.TopMoviesCount);
            var daily = await _unitOfWork.Statistics.GetRevenueForLastDaysAsync(_settings.ChartDays);

            return _mapper.CreateSummaryDto(tickets, revenue, snacks, movies, daily);
        }
    }
}