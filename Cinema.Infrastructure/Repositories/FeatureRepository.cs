using onlineCinema.Application.Interfaces;
using onlineCinema.Domain.Entities;
using onlineCinema.Infrastructure.Data;

namespace onlineCinema.Infrastructure.Repositories
{
    public class FeatureRepository
        : GenericRepository<Feature>, IFeatureRepository
    {
        private readonly ApplicationDbContext _db;

        public FeatureRepository(ApplicationDbContext db)
            : base(db)
        {
            _db = db;
        }
    }
}
