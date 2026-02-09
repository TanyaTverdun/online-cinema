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


        public async Task<Hall?> GetByIdWithStatsAsync(int id)
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
                throw new KeyNotFoundException("Hall not found");

            existingHall.HallNumber = hall.HallNumber;
            existingHall.RowCount = hall.RowCount;
            existingHall.SeatInRowCount = hall.SeatInRowCount;

            selectedFeatureIds ??= new List<int>();

            // remove unselected features
            var featuresToRemove = existingHall.HallFeatures
                .Where(hf => !selectedFeatureIds.Contains(hf.FeatureId))
                .ToList();

            foreach (var feature in featuresToRemove)
                _db.Remove(feature);

            // add new features
            var currentFeatureIds = existingHall.HallFeatures
                .Select(hf => hf.FeatureId)
                .ToList();

            var featuresToAdd = selectedFeatureIds
                .Where(id => !currentFeatureIds.Contains(id))
                .Select(id => new HallFeature
                {
                    HallId = hall.HallId,
                    FeatureId = id
                });

            foreach (var feature in featuresToAdd)
                existingHall.HallFeatures.Add(feature);

            await _db.SaveChangesAsync();
        }


        public async Task<IEnumerable<Hall>> GetAllWithStatsAsync()
        {
            return await _db.Halls
                .Include(h => h.HallFeatures)
                    .ThenInclude(hf => hf.Feature)
                .ToListAsync();
        }


        public async Task<Hall?> GetHallWithFutureSessionsAsync(int hallId)
        {
            var now = DateTime.Now;
            var threeDaysLater = now.AddDays(3);

            return await _db.Halls
                .Include(h => h.HallFeatures)
                    .ThenInclude(hf => hf.Feature)
                .Include(h => h.Sessions
                    .Where(s => s.ShowingDateTime >= now && s.ShowingDateTime <= threeDaysLater))
                    .ThenInclude(s => s.Movie)
                .FirstOrDefaultAsync(h => h.HallId == hallId);
        }

        public async Task<Hall?> GetByIdWithFeaturesAsync(int id)
        {
            return await _db.Halls
                .Include(h => h.HallFeatures)
                    .ThenInclude(hf => hf.Feature)
                .Include(h => h.Sessions)
                    .ThenInclude(s => s.Movie)
                .FirstOrDefaultAsync(h => h.HallId == id);
        }
      
    }
}
