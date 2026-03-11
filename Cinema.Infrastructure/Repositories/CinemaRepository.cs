using onlineCinema.Application.Interfaces;
using onlineCinema.Domain.Entities;
using onlineCinema.Infrastructure.Data;

namespace onlineCinema.Infrastructure.Repositories
{
    public class CinemaRepository
        : GenericRepository<Cinema>, ICinemaRepository
    {
        private readonly ApplicationDbContext _db;

        public CinemaRepository(ApplicationDbContext db)
            : base(db)
        {
            _db = db;
        }
    }
}
