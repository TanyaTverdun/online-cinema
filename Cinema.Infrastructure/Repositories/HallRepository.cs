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
    public class HallRepository : GenericRepository<Hall>, IHallRepository
    {
        private readonly ApplicationDbContext _db;

        public HallRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task DeleteAsync(int id)
        {
            var hall = await _db.Halls.FindAsync(id);
            if (hall != null)
            {
                _db.Halls.Remove(hall);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Hall>> GetAllForClientAsync()
        {
            throw new NotImplementedException();
            //return await _db.Halls
            //    .Include(h => h.HallFeatures)
            //    .ThenInclude(hf => hf.Feature)
            //    .ToListAsync();
        }

        public async Task<Hall?> GetByIdWithFeaturesAsync(int id)
        {
            return await _db.Halls
                .Include(h => h.HallFeatures)
                .ThenInclude(hf => hf.Feature)
                .FirstOrDefaultAsync(h => h.HallId == id);
        }

        public async Task UpdateWithFeaturesAsync(Hall hall, List<int> selectedFeatureIds)
        {
            var existingHall = await _db.Halls
                .Include(h => h.HallFeatures)
                .FirstOrDefaultAsync(h => h.HallId == hall.HallId);

            if (existingHall == null)
            {
                throw new KeyNotFoundException("Hall not found");
            }

            existingHall.HallNumber = hall.HallNumber;
            existingHall.RowCount = hall.RowCount;
            existingHall.SeatInRowCount = hall.SeatInRowCount;

            //remove unselected features
            var featuresToRemove = existingHall.HallFeatures
                .Where(hf => !selectedFeatureIds.Contains(hf.FeatureId))
                .ToList();

            foreach (var feature in featuresToRemove)
            {
                _db.Remove(feature);
            }

            //add new selected features
            var currentFeatureIds = existingHall.HallFeatures
                .Select(hf => hf.FeatureId)
                .ToList();

            var featuresToAdd = selectedFeatureIds
                .Where(id => !currentFeatureIds.Contains(id))
                .Select(id => new HallFeature
                {
                    HallId = hall.HallId,
                    FeatureId = id
                })
                .ToList();

            foreach (var feature in featuresToAdd)
            {
                existingHall.HallFeatures.Add(feature);
            }

            await _db.SaveChangesAsync();
        }
    }
}
