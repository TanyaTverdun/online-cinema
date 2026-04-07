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
    public class StudioBranchRepository
        : GenericRepository<StudioBranch>, IStudioBranchRepository
    {
        private readonly ApplicationDbContext _db;

        public StudioBranchRepository(ApplicationDbContext db)
            : base(db)
        {
            _db = db;
        }
        // Додамо метод для завантаження філії разом із її танцювальними залами
        public async Task<StudioBranch?> GetWithHallsAsync(int id)
        {
            return await _db.StudioBranches
                .Include(b => b.DanceHalls)
                .FirstOrDefaultAsync(b => b.BranchId == id);
        }
    }
}
