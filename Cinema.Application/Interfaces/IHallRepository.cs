using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Application.Interfaces
{
    public interface IHallRepository : IGenericRepository<Hall>
    {
        Task<Hall?> GetByIdWithFeaturesAsync(int id);

        Task<IEnumerable<Hall>> GetAllForClientAsync();

        Task UpdateWithFeaturesAsync(Hall hall, List<int> selectedFeatureIds);

        Task DeleteAsync(int id);
    }
}
