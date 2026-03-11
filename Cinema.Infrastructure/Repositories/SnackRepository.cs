using onlineCinema.Application.Interfaces;
using onlineCinema.Domain.Entities;
using onlineCinema.Infrastructure.Data;

namespace onlineCinema.Infrastructure.Repositories
{
    public class SnackRepository
        : GenericRepository<Snack>, ISnackRepository
    {
        private readonly ApplicationDbContext _db;

        public SnackRepository(ApplicationDbContext db)
            : base(db)
        {
            _db = db;
        }
    }
}
