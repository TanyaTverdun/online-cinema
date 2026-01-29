using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Application.DTOs;

namespace onlineCinema.Application.Services.Interfaces
{
    public interface ISessionService
    {
        Task<MovieScheduleDto> GetMovieScheduleAsync(int movieId);
        Task CreateSessionAsync(SessionCreateDto dto);

    }
}
