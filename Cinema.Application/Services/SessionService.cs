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
            var session = await _unitOfWork.Session.GetByIdWithMovieAndHallAsync(id);

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

            int days = dto.GenerateForWeek ? 7 : 1;

            for (int i = 0; i < days; i++)
            {
                var sessionDate = dto.ShowingDateTime.AddDays(i);

                var hasConflict = await _unitOfWork.Session
                    .HallHasSessionAtTimeAsync(
                        dto.HallId,
                        sessionDate,
                        movie.Runtime);

                if (hasConflict)
                {
                    throw new InvalidOperationException(
                        $"Конфлікт сеансу на дату {sessionDate:dd.MM.yyyy HH:mm}");
                }

                Session session = _mapper.MapToSession(dto);
                session.ShowingDateTime = sessionDate;

                await _unitOfWork.Session.AddAsync(session);
            }

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
                int movieId,
                int excludeSessionId = 0)
        {
            var movie = await _unitOfWork.Movie.GetByIdAsync(movieId);

            if (movie == null)
            {
                return false;
            }

            return await _unitOfWork.Session
                .HallHasSessionAtTimeAsync(
                    hallId,
                    dateTime,
                    movie.Runtime,
                    excludeSessionId);
        }

        public async Task<IEnumerable<SessionDto>> GetAllSessionsAsync()
        {
            var sessions = await _unitOfWork.Session.GetAllAsync(
                includeProperties: "Movie,Hall"
            );

            return _mapper.MapToDtoList(sessions);
        }

        public async Task DeleteSessionAsync(int id)
        {
            var session = await _unitOfWork.Session.GetByIdAsync(id);

            if (session == null)
            {
                throw new KeyNotFoundException("Сеанс не знайдено");
            }

            var hasTickets = (await _unitOfWork.Ticket.GetAllAsync(t => t.SessionId == id)).Any();

            if (hasTickets)
            {
                throw new InvalidOperationException("Не можна видалити сеанс, на який вже існують квитки.");
            }

            _unitOfWork.Session.Remove(session);
            await _unitOfWork.SaveAsync();
        }
    }
}
