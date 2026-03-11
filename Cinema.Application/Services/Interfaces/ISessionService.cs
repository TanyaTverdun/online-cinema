using onlineCinema.Application.DTOs.Session;

namespace onlineCinema.Application.Services.Interfaces
{
    public interface ISessionService
    {
        Task<MovieScheduleDto> GetMovieScheduleAsync(int movieId);
        Task CreateSessionAsync(SessionFormDto dto);
        Task<SessionDto> GetByIdAsync(int id);
        Task UpdateSessionAsync(SessionFormDto dto);
        Task<bool> HallHasSessionAtTime(
            int hallId,
            DateTime dateTime,
            int movieId,
            int excludeSessionId = 0);
        Task<IEnumerable<SessionDto>> GetAllSessionsAsync();
        Task DeleteSessionAsync(int id);
    }
}
