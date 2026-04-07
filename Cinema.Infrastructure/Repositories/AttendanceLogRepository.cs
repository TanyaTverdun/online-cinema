using Microsoft.EntityFrameworkCore;
using onlineCinema.Application.Interfaces;
using onlineCinema.Domain.Entities;
using onlineCinema.Domain.Enums;
using onlineCinema.Infrastructure.Data;

namespace onlineCinema.Infrastructure.Repositories
{
    public class AttendanceLogRepository 
            : GenericRepository<AttendanceLog>, IAttendanceLogRepository
    {
        private readonly ApplicationDbContext _db;

        public AttendanceLogRepository(ApplicationDbContext db) 
            : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<AttendanceLog>>
           GetLogsByClassIdAsync(int classId)
        {
            return await _db.AttendanceLogs
                .Where(t => t.ClassId == classId)
                .Include(t => t.Inventary)
                .ToListAsync();
        }
    }
}
