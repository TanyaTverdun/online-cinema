using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Application.Interfaces
{
    public interface IPerformanceRepository : IGenericRepository<Performance>
    {
        Task<Performance?> GetByIdWithAllDetailsAsync(int id);
    }
}
