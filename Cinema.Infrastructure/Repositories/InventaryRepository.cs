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
    public class InventaryRepository 
        : GenericRepository<Inventary>, IInventaryRepository
    {
        private readonly ApplicationDbContext _db;

        public InventaryRepository(ApplicationDbContext db) 
            : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Inventary>> GetInventaryByHallIdAsync(int hallId)
        {
            return await _db.Inventaries
                .Where(s => s.HallId == hallId)
                .OrderBy(s => s.Category)
                .ThenBy(s => s.IdentifiertNumber)
                .ToListAsync();
        }
    }
}
