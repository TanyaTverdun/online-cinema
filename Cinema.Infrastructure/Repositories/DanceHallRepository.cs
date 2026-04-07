using Microsoft.EntityFrameworkCore;
using onlineCinema.Application.DTOs;
using onlineCinema.Application.Interfaces;
using onlineCinema.Domain.Entities;
using onlineCinema.Infrastructure.Data;

namespace onlineCinema.Infrastructure.Repositories
{
    public class DanceHallRepository 
        : GenericRepository<DanceHall>, IDanceHallRepository
    {
        private readonly ApplicationDbContext _db;

        public DanceHallRepository(ApplicationDbContext db) 
            : base(db)
        {
            _db = db;
        }

        public async Task DeleteAsync(int id)
        {
            var hall = await _db.DanceHalls.FindAsync(id);
            if (hall != null)
            {
                _db.DanceHalls.Remove(hall);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<HallDto?> GetByIdWithStatsAsync(int id)
        {
            return await _db.DanceHalls
                .Where(h => h.HallId == id)
                .Select(h => new HallDto
                {
                    Id = h.HallId,
                    HallNumber = h.HallNumber,
                    AreaSize = h.AreaSize,
                    MaxPeople = h.MaxPeople,
                   

                    FeatureIds = h.HallEquipmentS
                        .Select(hf => hf.RequirementId)
                        .ToList(),

                    FeatureNames = h.HallEquipmentS
                        .Select(hf => hf.Requriment.RequirementName)
                        .ToList(),

                    FeatureDescriptions = h.HallEquipmentS
                        .Select(hf => hf.Requriment.RequirementName ?? "")
                        .ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<HallDto>> GetAllWithStatsAsync()
        {
            return await _db.DanceHalls
                .Select(h => new HallDto
                {
                    Id = h.HallId,
                    HallNumber = h.HallNumber,
                    AreaSize = h.AreaSize,
                    MaxPeople = h.MaxPeople,


                    FeatureNames = h.HallEquipmentS
                        .Select(hf => hf.Requriment.RequirementName)
                        .ToList(),

                    FeatureDescriptions = h.HallEquipmentS
                        .Select(hf => hf.Requriment.RequirementName ?? "")
                        .ToList()
                })
                .ToListAsync();
        }

        public async Task UpdateWithFeaturesAsync(
            DanceHall hall, 
            List<int> selectedFeatureIds)
        {
            var existingHall = await _db.DanceHalls
                .Include(h => h.HallEquipmentS)
                .Include(h => h.Inventaries)
                .FirstOrDefaultAsync(h => h.HallId == hall.HallId);

            if (existingHall == null)
            {
                throw new KeyNotFoundException("Зал не знайдено.");
            }

            existingHall.HallNumber = hall.HallNumber;
            existingHall.AreaSize = hall.AreaSize;
            existingHall.MaxPeople = hall.MaxPeople;
            

            

            selectedFeatureIds ??= new List<int>();

            var featuresToRemove = existingHall.HallEquipmentS
                .Where(hf => !selectedFeatureIds.Contains(hf.RequirementId))
                .ToList();

            _db.RemoveRange(featuresToRemove);

            var currentFeatureIds = existingHall.HallEquipmentS
                .Select(hf => hf.RequirementId)
                .ToList();

            var featuresToAdd = selectedFeatureIds
                .Where(id => !currentFeatureIds.Contains(id))
                .Select(id => new HallEquipment
                {
                    HallId = hall.HallId,
                    RequirementId = id
                });

            await _db.AddRangeAsync(featuresToAdd);

            await _db.SaveChangesAsync();
        }

        public async Task<HallDto?> GetHallWithFutureSessionsAsync(int hallId)
        {
            var now = DateTime.Now;
            var threeDaysLater = now.AddDays(3);

            return await _db.DanceHalls
                .Where(h => h.HallId == hallId)
                .Select(h => new HallDto
                {
                    Id = h.HallId,
                    HallNumber = h.HallNumber,
                    AreaSize = h.AreaSize,
                    MaxPeople = h.MaxPeople,


                    FeatureNames = h.HallEquipmentS
                        .Select(hf => hf.Requriment.RequirementName)
                        .ToList(),

                    FeatureDescriptions = h.HallEquipmentS
                        .Select(hf => hf.Requriment.RequirementName ?? "")
                        .ToList(),

                    Sessions = h.DanceClasses
                        .Where(s => s.StartDateTime >= now &&
                                    s.StartDateTime <= threeDaysLater)
                        .OrderBy(s => s.StartDateTime)
                        .Select(s => new DanceClassMapDto
                        {
                            ClassId = s.ClassId,
                            StartDateTime = s.StartDateTime,
                            PerformanceTitle = s.Performance.Title

                        }).ToList()

                })
                .FirstOrDefaultAsync();
        }
    }
}
