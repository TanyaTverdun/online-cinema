
using onlineCinema.Application.DTOs;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Application.Interfaces
{
    public interface IHallRepository : IGenericRepository<Hall>
    {
        Task<Hall> GetByIdWithStatsAsync(int id);

        Task UpdateWithFeaturesAsync(Hall hall, List<int> selectedFeatureIds);

        Task DeleteAsync(int id);

        Task<IEnumerable<Hall>> GetAllWithStatsAsync();

        Task<Hall> GetHallWithFutureSessionsAsync(int hallId);
        Task<Hall?> GetByIdWithFeaturesAsync(int id);

    }
}
