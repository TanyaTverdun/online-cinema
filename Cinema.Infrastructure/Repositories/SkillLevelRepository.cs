using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using onlineCinema.Application.Interfaces;
using onlineCinema.Domain.Entities;
using onlineCinema.Infrastructure.Data;

namespace onlineCinema.Infrastructure.Repositories
{
    public class SkillLevelRepository 
        : GenericRepository<SkillLevel>, ISkillLevelRepository
    {
        private readonly ApplicationDbContext _db;

        public SkillLevelRepository(ApplicationDbContext db) 
            : base(db)
        {
            _db = db;
        }
        // Метод для отримання рівня разом із танцями, які до нього належать
        public async Task<SkillLevel?> GetWithPerformancesAsync(int id)
        {
            return await _db.SkillLevels
                .Include(sl => sl.PerformanceLevels)
                    .ThenInclude(pl => pl.Performance)
                .FirstOrDefaultAsync(sl => sl.LevelId == id);
        }
    }
}
