using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Application.Interfaces
{
    public interface IFeatureRepository
    {
        Task<IEnumerable<Feature>> GetAllAsync();
        Task<Feature?> GetByIdAsync(int id);
        Task AddAsync(Feature feature);
        Task UpdateAsync(Feature feature);
        Task DeleteAsync(int id);
    }
}
