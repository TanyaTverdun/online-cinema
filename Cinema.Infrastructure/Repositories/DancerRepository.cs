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
    public class DancerRepository 
        : GenericRepository<Dancer>, IDancerRepository
    {
        private readonly ApplicationDbContext _db;

        public DancerRepository(ApplicationDbContext db) 
            : base(db)
        {
            _db = db;
        }
        // Тут зазвичай додають метод для отримання танцюриста з його виступами
        public async Task<Dancer?> GetDancerWithPerformancesAsync(int id)
        {
            return await _db.Dancers
                .Include(d => d.PerformanceDancers)
                    .ThenInclude(pd => pd.Performance)
                .FirstOrDefaultAsync(d => d.DancerId == id);
        }
    }
}
