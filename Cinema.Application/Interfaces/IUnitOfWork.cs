using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineCinema.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ISessionRepository Sessions { get; }
        IMovieRepository Movies { get; }
        IHallRepository Halls { get; }
        
        Task<int> SaveChangesAsync();
    }
}
