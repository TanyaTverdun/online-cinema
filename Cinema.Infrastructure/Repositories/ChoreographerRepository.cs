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
    public class ChoreographerRepository 
        : GenericRepository<Choreographer>, IChoreographerRepository
    {
        private readonly ApplicationDbContext _db;

        public ChoreographerRepository(ApplicationDbContext db) 
            : base(db)
        {
            _db = db;
        }
        // Додамо метод для отримання хореографа разом із його постановками
        public async Task<Choreographer?> GetWithPerformancesAsync(int id)
        {
            return await _db.Choreographers
                .Include(c => c.ChoreographerPerformances)
                    .ThenInclude(cp => cp.Performance)
                .FirstOrDefaultAsync(c => c.ChoreographerId == id);
        }
    }
}
