using onlineCinema.Application.DTOs;
using onlineCinema.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Application.Interfaces
{
    public interface ISessionRepository
    {
        Task<IEnumerable<Session>> GetAllAsync();
        Task<IEnumerable<Session>> GetAllWithDetailsAsync();
        Task<IEnumerable<Session>> GetByMovieIdAsync(int movieId);
        Task<Session?> GetByIdAsync(int id);

        Task<IEnumerable<Session>> GetScheduleAsync();
    }
}
