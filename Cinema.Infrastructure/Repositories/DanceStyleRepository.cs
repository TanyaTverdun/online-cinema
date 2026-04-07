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
    public class DanceStyleRepository 
        : GenericRepository<DanceStyle>, IDanceStyleRepository
    {
        private readonly ApplicationDbContext _db;

        public DanceStyleRepository(ApplicationDbContext db) 
            : base(db)
        {
            _db = db;
        }
        // Метод для отримання стилю разом із виступами/танцями в цьому стилі
        public async Task<DanceStyle?> GetWithPerformancesAsync(int id)
        {
            return await _db.DanceStyles
                .Include(ds => ds.PerformanceStyles)
                    .ThenInclude(ps => ps.Performance)
                .FirstOrDefaultAsync(ds => ds.StyleId == id);
        }
    }
}
