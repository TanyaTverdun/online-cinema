using Microsoft.EntityFrameworkCore;
using onlineCinema.Application.Interfaces;
using onlineCinema.Domain.Entities;
using onlineCinema.Infrastructure.Data;

namespace onlineCinema.Infrastructure.Repositories
{
    public class DanceClassRepository 
        : GenericRepository<DanceClass>, IDanceClassRepository
    {
        private readonly ApplicationDbContext _db;

        public DanceClassRepository(ApplicationDbContext db) 
            : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<DanceClass>> GetFutureClassAsync()
        {
            return await _db.DanceClasses
                .Include(s => s.Performance)
                .Include(s => s.DancerHall)
                .Where(s => s.StartDateTime > DateTime.Now)
                .OrderBy(s => s.StartDateTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<DanceClass>> 
            GetFutureClassesByPerformanceIdAsync(int performanceId)
        {
            return await _db.DanceClasses
                .Where(s => s.PerformanceId == performanceId
                    && s.StartDateTime > DateTime.Now)
                .Include(s => s.DancerHall)
                    .ThenInclude(hf => hf.HallEquipmentS)
                        .ThenInclude(f => f.Requriment)
                .OrderBy(s => s.StartDateTime)
                .ToListAsync();
        }

        public async Task<DanceClass?> GetByIdWithPerformanceAndHallAsync(int classId)
        {
            return await _db.DanceClasses
                .Include(s => s.Performance)
                .Include(s => s.DancerHall)
                .FirstOrDefaultAsync(s => s.ClassId == classId);
        }

        public async Task<bool> HallHasClassAtTimeAsync(
            int hallId,
            DateTime startDateTime,
            int durationMinutes,
            int excludeClassId = 0)

        {
            var newClassEnd = startDateTime.AddMinutes(durationMinutes);
            var date = startDateTime.Date;

            return await _db.DanceClasses
                .Include(s => s.Performance)
                .AnyAsync(s =>
                    s.HallId == hallId &&
                    startDateTime <
                        s.StartDateTime.AddMinutes(s.Performance.Duraction) &&
                    newClassEnd > s.StartDateTime
                );
        }

        //public async Task<bool> HallHasClassAtTimeAsync(
        //    int hallId,
        //    DateTime startDateTime,
        //    int durationMinutes,
        //    int excludeClassId = 0)
        //{
        //    var newEnd = startDateTime.AddMinutes(durationMinutes);
        //    var date = startDateTime.Date;

        //    return await _db.DanceClasses
        //        .Include(s => s.Performance)
        //        .Where(s =>
        //            s.HallId == hallId &&
        //            s.ClassId != excludeClassId &&
        //            s.StartDateTime.Date == date
        //        )
        //        .AnyAsync(s =>
        //            startDateTime < s.StartDateTime.AddMinutes(s.Performance.Duraction)
        //                && newEnd > s.StartDateTime
        //        );
        //}

    }
}
