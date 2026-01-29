using onlineCinema.Application.DTOs;
using onlineCinema.Application.Interfaces;
using onlineCinema.Application.Mapping;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Application.Services
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SessionMapper _mapper;

        public SessionService(
            IUnitOfWork unitOfWork,
            SessionMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SessionDto> GetByIdAsync(int id)
        {
            var session = await _unitOfWork.Session.GetByIdAsync(id);

            if (session == null)
            {
                throw new KeyNotFoundException("Сеанс не знайдено");
            }

            return _mapper.MapToDto(session);
        }

        public async Task<MovieScheduleDto> GetMovieScheduleAsync(int movieId)
        {
            var movie = await _unitOfWork.Movie.GetByIdAsync(movieId);
            if (movie == null)
            {
                throw new KeyNotFoundException($"Фільм з ID {movieId} не знайдено.");
            }

            var sessions = await _unitOfWork.Session.GetFutureSessionsByMovieIdAsync(movieId);

            var scheduleDto = _mapper.MapToMovieSchedule(movie, sessions);

            return scheduleDto;
        }

        public async Task CreateSessionAsync(SessionCreateDto dto)
        {
            var movie = await _unitOfWork.Movie.GetByIdAsync(dto.MovieId);

            if (movie == null)
            {
                throw new KeyNotFoundException("Фільм не знайдено");
            }

            var hasConflict = await _unitOfWork.Session
                .HallHasSessionAtTimeAsync(
                    dto.HallId,
                    dto.ShowingDateTime,
                    movie.Runtime);

            if (hasConflict)
            {
                throw new InvalidOperationException(
                    "У цьому залі вже існує сеанс, який перетинається за часом");
            }

            Session session = _mapper.MapToSession(dto);

            await _unitOfWork.Session.AddAsync(session);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateSessionAsync(SessionUpdateDto dto)
        {
            var session = await _unitOfWork.Session.GetByIdAsync(dto.Id);

            if (session == null)
            {
                throw new KeyNotFoundException("Сеанс не знайдено");
            }

            var movie = await _unitOfWork.Movie.GetByIdAsync(dto.MovieId);

            if (movie == null)
            {
                throw new KeyNotFoundException("Фільм не знайдено");
            }

            var hasConflict = await _unitOfWork.Session
                .HallHasSessionAtTimeAsync(
                    dto.HallId,
                    dto.ShowingDateTime,
                    movie.Runtime,
                    dto.Id);

            if (hasConflict)
            {
                throw new InvalidOperationException(
                    "У цьому залі вже є інший сеанс, що перетинається за часом");
            }

            _mapper.UpdateEntityFromDto(dto, session);

            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> HallHasSessionAtTime(
            int hallId,
            DateTime dateTime,
            int excludeSessionId)
        {
            var session = await _unitOfWork.Session.GetByIdAsync(excludeSessionId);

            if (session == null)
            {
                return false;
            }

            var movie = await _unitOfWork.Movie.GetByIdAsync(session.MovieId);

            if (movie == null)
            {
                return false;
            }

            bool isHallHasSessionAtTime = await _unitOfWork.Session
                .HallHasSessionAtTimeAsync(
                    hallId,
                    dateTime,
                    movie.Runtime,
                    excludeSessionId);

            return isHallHasSessionAtTime;
        }
    }
}
