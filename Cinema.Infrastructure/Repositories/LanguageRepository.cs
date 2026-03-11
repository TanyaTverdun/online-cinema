using onlineCinema.Application.Interfaces;
using onlineCinema.Domain.Entities;
using onlineCinema.Infrastructure.Data;

namespace onlineCinema.Infrastructure.Repositories
{
    public class LanguageRepository
        : GenericRepository<Language>, ILanguageRepository
    {
        private readonly ApplicationDbContext _db;

        public LanguageRepository(ApplicationDbContext db)
            : base(db)
        {
            _db = db;
        }
    }
}
