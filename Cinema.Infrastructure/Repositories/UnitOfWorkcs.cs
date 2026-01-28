using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Application.Interfaces;
using onlineCinema.Infrastructure.Data;

namespace onlineCinema.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public ISessionRepository Sessions { get; private set; }
        public IMovieRepository Movies { get; private set; }
        public IHallRepository Halls { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Sessions = new SessionRepository(_context);
            Movies = new MovieRepository(_context);
            Halls = new HallRepository(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}