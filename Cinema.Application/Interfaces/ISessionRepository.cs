using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Application.Interfaces
{
    public interface ISessionRepository : IGenericRepository<Session>
    {
        Task<IEnumerable<Session>> GetFutureSessionsAsync();
        Task<IEnumerable<Session>> GetFutureSessionsByMovieIdAsync(int movieId);
        Task<Session?> GetByIdWithMovieAndHallAsync(int sessionId);
        Task<bool> HallHasSessionAtTimeAsync(
            int hallId,
            DateTime showingDateTime,
            int movieDurationMinutes);
        Task<bool> HallHasSessionAtTimeAsync(
            int hallId,
            DateTime showingDateTime,
            int movieDurationMinutes,
            int excludeSessionId);
    }
}

