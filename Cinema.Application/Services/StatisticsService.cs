using Microsoft.Extensions.Options;
using onlineCinema.Application.Configurations;
using onlineCinema.Application.DTOs.AdminStatistics;
using onlineCinema.Application.Interfaces;
using onlineCinema.Application.Mapping;
using onlineCinema.Application.Services.Interfaces;

namespace onlineCinema.Application.Services;

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
        // 1. Завантаженість залів (середня кількість людей на заняттях)
        var occupancy = await _unitOfWork.Statistics
            .GetAverageOccupancyPerPerformanceAsync(_settings.HallOccupancyCount);

        // 2. Найпопулярніші та найменш популярні постановки/курси
        var popPerformances = await _unitOfWork.Statistics
            .GetPerformancesByPopularityAsync(_settings.TopPerformancesCount, ascending: false);
        var leastPerformances = await _unitOfWork.Statistics
            .GetPerformancesByPopularityAsync(_settings.TopPerformancesCount, ascending: true);

        // 3. Найпопулярніші та найменш популярні хореографи (за кількістю учнів)
        var popInstructors = await _unitOfWork.Statistics
            .GetInstructorsByPopularityAsync(_settings.TopInstructorsCount, ascending: false);
        var leastInstructors = await _unitOfWork.Statistics
            .GetInstructorsByPopularityAsync(_settings.TopInstructorsCount, ascending: true);

        // Повертаємо маппінг у DTO
        return _mapper.CreateSummaryDto(
            occupancy,
            popPerformances,
            leastPerformances,
            popInstructors,
            leastInstructors
        );
    }
}