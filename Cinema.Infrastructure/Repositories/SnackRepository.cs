using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using onlineCinema.Domain.Entities;
using onlineCinema.Infrastructure.Data;

namespace onlineCinema.Infrastructure.Repositories
{
    public class SnackRepository : GenericRepository<Snack>, ISnackRepository
    {
        private readonly ApplicationDbContext _db;

        public SnackRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
