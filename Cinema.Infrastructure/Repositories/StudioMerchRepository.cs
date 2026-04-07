using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using onlineCinema.Domain.Entities;
using onlineCinema.Infrastructure.Data;

namespace onlineCinema.Infrastructure.Repositories
{
    public class StudioMerchRepository 
        : GenericRepository<StudioMerch>, IStudioMerchRepository
    {
        private readonly ApplicationDbContext _db;

        public StudioMerchRepository(ApplicationDbContext db) 
            : base(db)
        {
            _db = db;
        }
        // Можна додати метод для отримання популярного мерчу або за категоріями
        public async Task<IEnumerable<StudioMerch>> GetAvailableMerchAsync()
        {
            return await _db.StudioMerches
                .Where(m => m.Price > 0) // Наприклад, тільки платні позиції
                .ToListAsync();
        }
    }
}
