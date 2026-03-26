using onlineCinema.Application.DTOs.Hall;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Application.Interfaces
{
    public interface IHallRepository : IGenericRepository<Hall>
    {
        Task<HallDto?> GetByIdWithStatsAsync(int id);
        Task UpdateWithFeaturesAsync(Hall hall, List<int> selectedFeatureIds);
        Task DeleteAsync(int id);
        Task<IEnumerable<HallDto>> GetAllWithStatsAsync();
        Task<HallDto?> GetHallWithFutureSessionsAsync(int hallId, int daysAhead);
    }
}
