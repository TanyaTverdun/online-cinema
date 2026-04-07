using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using onlineCinema.Application.Interfaces;
using onlineCinema.Domain.Entities;
using onlineCinema.Infrastructure.Data;

namespace onlineCinema.Infrastructure.Repositories
{
    public class RequrimentRepository 
        : GenericRepository<Requriment>, IRequrimentRepository
    {
        private readonly ApplicationDbContext _db;

        public RequrimentRepository(ApplicationDbContext db) 
            : base(db)
        {
            _db = db;
        }
        // Метод для отримання вимоги разом із залами, де вона реалізована
        public async Task<Requriment?> GetWithHallsAsync(int id)
        {
            return await _db.Requriments
                .Include(r => r.HallEquipment)
                    .ThenInclude(he => he.DanceHall)
                .FirstOrDefaultAsync(r => r.RequirementId == id);
        }
    }
}
