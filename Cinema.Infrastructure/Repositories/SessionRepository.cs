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
    public class SessionRepository : ISessionRepository
    {
        private readonly ApplicationDbContext _db;

        public SessionRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Session>> GetAllAsync()
        {
            return await _db.Sessions
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .ToListAsync();
        }

        public async Task<IEnumerable<Session>> GetByMovieIdAsync(int movieId)
        {
            return await _db.Sessions
                .Where(s => s.MovieId == movieId)
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .ToListAsync();
        }

        public async Task<Session?> GetByIdAsync(int id)
        {
            return await _db.Sessions
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .FirstOrDefaultAsync(s => s.SessionId == id);
        }
    }
}
