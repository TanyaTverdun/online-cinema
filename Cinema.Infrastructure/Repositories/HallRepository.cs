using Microsoft.EntityFrameworkCore;
using onlineCinema.Application.DTOs.Hall;
using onlineCinema.Application.DTOs.Session;
using onlineCinema.Application.Interfaces;
using onlineCinema.Application.Services.Interfaces;
using onlineCinema.Domain.Entities;
using onlineCinema.Infrastructure.Data;

namespace onlineCinema.Infrastructure.Repositories
{
    public class HallRepository
        : GenericRepository<Hall>, IHallRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ITimeProvider _timeProvider;

        public HallRepository(ApplicationDbContext db, ITimeProvider timeProvider)
            : base(db)
        {
            _db = db;
            _timeProvider = timeProvider;
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

        public async Task<HallDto?> GetByIdWithStatsAsync(int id)
        {
            return await _db.Halls
                .Where(h => h.HallId == id)
                .Select(h => new HallDto
                {
                    Id = h.HallId,
                    HallNumber = h.HallNumber,
                    RowCount = h.RowCount,
                    SeatInRowCount = h.SeatInRowCount,


                    FeatureIds = h.HallFeatures
                        .Select(hf => hf.FeatureId)
                        .ToList(),

                    FeatureNames = h.HallFeatures
                        .Select(hf => hf.Feature.Name)
                        .ToList(),

                    FeatureDescriptions = h.HallFeatures
                        .Select(hf => hf.Feature.Description ?? "")
                        .ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<HallDto>> GetAllWithStatsAsync()
        {
            return await _db.Halls
                .Select(h => new HallDto
                {
                    Id = h.HallId,
                    HallNumber = h.HallNumber,
                    RowCount = h.RowCount,
                    SeatInRowCount = h.SeatInRowCount,


                    FeatureNames = h.HallFeatures
                        .Select(hf => hf.Feature.Name)
                        .ToList(),

                    FeatureDescriptions = h.HallFeatures
                        .Select(hf => hf.Feature.Description ?? "")
                        .ToList()
                })
                .ToListAsync();
        }

        public async Task UpdateWithFeaturesAsync(
            Hall hall,
            List<int> selectedFeatureIds)
        {
            var existingHall = await _db.Halls
                .Include(h => h.HallFeatures)
                .Include(h => h.Seats)
                .FirstOrDefaultAsync(h => h.HallId == hall.HallId);

            if (existingHall == null)
            {
                throw new KeyNotFoundException("Зал не знайдено.");
            }

            existingHall.HallNumber = hall.HallNumber;
            existingHall.RowCount = hall.RowCount;
            existingHall.SeatInRowCount = hall.SeatInRowCount;




            selectedFeatureIds ??= new List<int>();

            var featuresToRemove = existingHall.HallFeatures
                .Where(hf => !selectedFeatureIds.Contains(hf.FeatureId))
                .ToList();

            _db.RemoveRange(featuresToRemove);

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

            await _db.AddRangeAsync(featuresToAdd);

            await _db.SaveChangesAsync();
        }

        public async Task<HallDto?> GetHallWithFutureSessionsAsync(int hallId, int daysAhead)
        {
            var now = _timeProvider.Now;
            var endDate = now.AddDays(daysAhead);

            return await _db.Halls
                .Where(h => h.HallId == hallId)
                .Select(h => new HallDto
                {
                    Id = h.HallId,
                    HallNumber = h.HallNumber,
                    RowCount = h.RowCount,
                    SeatInRowCount = h.SeatInRowCount,


                    FeatureNames = h.HallFeatures
                        .Select(hf => hf.Feature.Name)
                        .ToList(),

                    FeatureDescriptions = h.HallFeatures
                        .Select(hf => hf.Feature.Description ?? "")
                        .ToList(),

                    Sessions = h.Sessions
                        .Where(s => s.ShowingDateTime >= now &&
                                    s.ShowingDateTime <= endDate)
                        .OrderBy(s => s.ShowingDateTime)
                        .Select(s => new SessionSeatMapDto
                        {
                            SessionId = s.SessionId,
                            ShowingDate = s.ShowingDateTime,
                            MovieTitle = s.Movie.Title,

                        }).ToList()

                })
                .FirstOrDefaultAsync();
        }
    }
}
