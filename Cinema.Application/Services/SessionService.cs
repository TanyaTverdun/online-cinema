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
            var movie = await _unitOfWork.Movie.GetByIdWithFeaturesAsync(dto.MovieId);
            if (movie == null)
            {
                throw new KeyNotFoundException("Фільм не знайдено");
            }

            var hall = await _unitOfWork.Hall.GetByIdWithFeaturesAsync(dto.HallId);
            if (hall == null)
            {
                throw new KeyNotFoundException("Зал не знайдено");
            }

            ValidateHallFeatures(movie, hall);

            var dates = dto.GenerateForWeek
                ? Enumerable.Range(0, 7)
                    .Select(i => dto.ShowingDateTime.AddDays(i))
                : new List<DateTime> { dto.ShowingDateTime };

            var conflicts = new List<DateTime>();

            foreach (var date in dates)
            {
                var hasConflict = await _unitOfWork.Session
                    .HallHasSessionAtTimeAsync(
                        dto.HallId,
                        date,
                        movie.Runtime);

                if (hasConflict)
                {
                    conflicts.Add(date);
                    continue;
                }

                var session = new Session
                {
                    MovieId = dto.MovieId,
                    HallId = dto.HallId,
                    ShowingDateTime = date,
                    BasePrice = dto.BasePrice
                };

                await _unitOfWork.Session.AddAsync(session);
            }

            await _unitOfWork.SaveAsync();

            if (conflicts.Any())
            {
                var message =
                    "Деякі дати пропущені через конфлікти:\n" +
                    string.Join("\n", conflicts.Select(d => d.ToString("dd.MM.yyyy HH:mm")));

                throw new InvalidOperationException(message);
            }
        }

        private void ValidateHallFeatures(Movie movie, Hall hall)
        {
            var movieFeatureIds = movie.MovieFeatures
                .Select(mf => mf.FeatureId)
                .ToHashSet();

            var hallFeatureIds = hall.HallFeatures
                .Select(hf => hf.FeatureId)
                .ToHashSet();

            var missingFeatures = movieFeatureIds.Except(hallFeatureIds).ToList();

            if (missingFeatures.Any())
            {
                throw new InvalidOperationException(
                    "Зал не підтримує всі необхідні характеристики фільму");
            }
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
