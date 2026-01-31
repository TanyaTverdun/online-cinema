using Microsoft.EntityFrameworkCore;
using onlineCinema.Application.Interfaces;
using onlineCinema.Domain.Entities;
using onlineCinema.Infrastructure.Data;

namespace onlineCinema.Infrastructure.Repositories
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly ApplicationDbContext _db;

        public SessionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Session>> GetFutureSessionsAsync()
        {
            return await _db.Sessions
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .Where(s => s.ShowingDateTime > DateTime.Now)
                .OrderBy(s => s.ShowingDateTime)
                .ToListAsync();
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

        public async Task<bool> HallHasSessionAtTimeAsync(
            int hallId,
            DateTime showingDateTime,
            int movieDurationMinutes)
        {
            var newSessionEnd = showingDateTime
                .AddMinutes(movieDurationMinutes);

            return await _db.Sessions
                .Include(s => s.Movie)
                .AnyAsync(s =>
                    s.HallId == hallId &&
                    showingDateTime <
                        s.ShowingDateTime.AddMinutes(s.Movie.Runtime) &&
                    newSessionEnd > s.ShowingDateTime
                );
        }

        public async Task<bool> HallHasSessionAtTimeAsync(
            int hallId,
            DateTime showingDateTime,
            int movieDurationMinutes,
            int excludeSessionId = 0)
        {
            var newEnd = showingDateTime.AddMinutes(movieDurationMinutes);
            var date = showingDateTime.Date;

            return await _db.Sessions
                .Include(s => s.Movie)
                .Where(s =>
                    s.HallId == hallId &&
                    s.SessionId != excludeSessionId &&
                    s.ShowingDateTime.Date == date
                )
                .AnyAsync(s =>
                    showingDateTime < s.ShowingDateTime.AddMinutes(s.Movie.Runtime) &&
                    newEnd > s.ShowingDateTime
                );
        }

    }
}
