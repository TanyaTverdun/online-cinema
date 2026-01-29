using Microsoft.EntityFrameworkCore;
using onlineCinema.Application.DTOs;
using onlineCinema.Application.Interfaces;
using onlineCinema.Domain.Entities;
using onlineCinema.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                     FeatureNames = h.HallFeatures.Select(hf => hf.Feature.Name).ToList(),
                     FeatureIds = h.HallFeatures.Select(hf => hf.FeatureId).ToList()
                 })
                 .FirstOrDefaultAsync();
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
                        .ToList()
                })
                .ToListAsync();
        }

        public async Task<HallDto?> GetHallWithFutureSessionsAsync(int hallId)
        {
            var now = DateTime.Now;
            var threeDaysLater = now.AddDays(3);

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

                    Sessions = h.Sessions
                        .Where(s => s.ShowingDateTime >= now && s.ShowingDateTime <= threeDaysLater)
                        .OrderBy(s => s.ShowingDateTime)
                        .Select(s => new SessionSeatMapDto
                        {
                            SessionId = s.SessionId,
                            ShowingDate = s.ShowingDateTime,
                            MovieTitle = s.Movie.Title,

                            //Seats = s.Seats
                            //    .Select(ss => new SeatDto
                            //    {
                            //        SeatId = ss.SeatId,
                            //        Row = ss.RowNumber,
                            //        Number = ss.SeatNumber,
                            //        IsBooked = ss.IsBooked
                            //    })
                            //    .ToList()
                        }).ToList()

                })
                .FirstOrDefaultAsync();
        }
    }
}
