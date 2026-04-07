using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Application.Interfaces
{
    public interface IDanceClassRepository : IGenericRepository<DanceClass>
    {
        Task<IEnumerable<DanceClass>> GetFutureClassAsync();
        Task<IEnumerable<DanceClass>> GetFutureClassesByPerformanceIdAsync(int performanceId);
        Task<DanceClass?> GetByIdWithPerformanceAndHallAsync(int classId);
        //Task<bool> HallHasClassAtTimeAsync(
        //    int hallId,
        //   DateTime startDateTime,
        //    int durationMinutes);
        Task<bool> HallHasClassAtTimeAsync(
            int hallId,
           DateTime startDateTime,
            int durationMinutes,
        int excludeClassId);
    }
}

