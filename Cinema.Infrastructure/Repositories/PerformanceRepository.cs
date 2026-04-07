using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Application.Interfaces;
using onlineCinema.Domain.Entities;
using onlineCinema.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace onlineCinema.Infrastructure.Repositories
{
    public class PerformanceRepository 
        : GenericRepository<Performance>, IPerformanceRepository
    {
        private readonly ApplicationDbContext _db;

        public PerformanceRepository(ApplicationDbContext db) 
            : base(db)
        {
            _db = db;
        }

        public async Task<Performance?> GetByIdWithAllDetailsAsync(int id)
        {
            return await _db.Performances
                    .Include(p => p.PerformanceStyles)
                    .ThenInclude(ps => ps.StyleId)
                    .Include(p => p.PerformanceDancers)
                    .ThenInclude(pd => pd.Dancer)
                    .Include(p => p.ChoreographerPerformances)
                    .ThenInclude(cp => cp.Choreographer)
                    .Include(p => p.PerformanceLevels)
                    .ThenInclude(pl => pl.LevelId)
                    .Include(p => p.PerformanceRequirements)
                    .ThenInclude(pr => pr.RequirementId)
                .FirstOrDefaultAsync(p => p.PerformanceId == id); 
        }
    }
}
