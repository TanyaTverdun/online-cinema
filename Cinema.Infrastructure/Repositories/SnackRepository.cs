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
    public class SnackRepository : ISnackRepository
    {
        private readonly ApplicationDbContext _context;

        public SnackRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Snack>> GetAllAsync()
        {
            return await _context.Snacks.ToListAsync();
        }

        public async Task<Snack?> GetByIdAsync(int id)
        {
            return await _context.Snacks.FirstOrDefaultAsync(s => s.SnackId == id);
        }
    }
}
