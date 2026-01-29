using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Application.DTOs;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Application.Interfaces
{
    public interface IHallRepository : IGenericRepository<Hall>
    {
        Task<HallDto?> GetByIdWithStatsAsync(int id);

        Task UpdateWithFeaturesAsync(Hall hall, List<int> selectedFeatureIds);

        Task DeleteAsync(int id);

        Task<IEnumerable<HallDto>> GetAllWithStatsAsync();

        Task<HallDto?> GetHallWithFutureSessionsAsync(int hallId);
    }
}
