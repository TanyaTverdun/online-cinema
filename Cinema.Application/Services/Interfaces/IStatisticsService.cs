using onlineCinema.Application.DTOs.AdminStatistics;

namespace onlineCinema.Application.Services.Interfaces
{
    public interface IStatisticsService
    {
        Task<AdminStatisticsDto> GetAdminStatisticsAsync();
    }
}
