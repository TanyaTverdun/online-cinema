using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Application.Interfaces;
using onlineCinema.Infrastructure.Data;

namespace onlineCinema.Infrastructure.Repositories
{
    public class HallRepository : IHallRepository
    {
        private readonly ApplicationDbContext _context;

        public HallRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
    }
}
