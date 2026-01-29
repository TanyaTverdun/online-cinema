using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Application.DTOs;
using onlineCinema.Application.Interfaces;
using onlineCinema.Application.Mapping;
using onlineCinema.Application.Services.Interfaces;

namespace onlineCinema.Application.Services
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SessionMapper _mapper;

        public SessionService(IUnitOfWork unitOfWork, SessionMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
    }
}
