using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Application.Interfaces
{
    public interface ISessionRepository : IGenericRepository<Session>
    {
        //майбутні сеанси 
        Task<IEnumerable<Session>> GetFutureSessionsByMovieIdAsync(int movieId);
    }
}