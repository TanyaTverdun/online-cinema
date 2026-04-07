using onlineCinema.Application.DTOs.AdminStatistics;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Application.Interfaces
{
    public interface IStatisticRepository : IGenericRepository<FinancialTransaction>
    {
        // Популярність виступів/курсів
        Task<List<TopItemDto>> GetPerformancesByPopularityAsync(int count, bool ascending);

        // Популярність мерчу або інструкторів (залежно від того, що викликаєш у сервісі)
        Task<List<TopItemDto>> GetInstructorsByPopularityAsync(int count, bool ascending);

        // Середня заповнюваність (кількість людей на заняттях) — ЦЬОГО НЕ ВИСТАЧАЛО
        Task<List<OccupancyDto>> GetAverageOccupancyPerPerformanceAsync(int count);
    }
}