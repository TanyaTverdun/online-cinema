using Microsoft.EntityFrameworkCore;
using onlineCinema.Application.Interfaces;
using onlineCinema.Domain.Entities;
using onlineCinema.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Infrastructure.Repositories
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly ApplicationDbContext _db;

        public SessionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Session>> GetFutureSessionsByMovieIdAsync(int movieId)
        {
            return await _db.Sessions
                .Where(s => s.MovieId == movieId && s.ShowingDateTime > DateTime.Now)
                .Include(s => s.Hall)
                    .ThenInclude(hf => hf.HallFeatures)
                        .ThenInclude(f => f.Feature)
                .OrderBy(s => s.ShowingDateTime)
                .ToListAsync();
        }

        public async Task<Session?> GetByIdWithMovieAndHallAsync(int sessionId)
        {
            return await _db.Sessions
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .FirstOrDefaultAsync(s => s.SessionId == sessionId);
        }
    }
}
