using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Application.Interfaces;
using onlineCinema.Domain.Entities;
using onlineCinema.Infrastructure.Data;

namespace onlineCinema.Infrastructure.Repositories
{
    public class DirectorRepository 
        : GenericRepository<Director>, IDirectorRepository
    {
        private readonly ApplicationDbContext _db;

        public DirectorRepository(ApplicationDbContext db) 
            : base(db)
        {
            _db = db;
        }
    }
}
