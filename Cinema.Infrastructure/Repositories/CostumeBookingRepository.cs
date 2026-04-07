using Microsoft.EntityFrameworkCore;
using onlineCinema.Application.Interfaces;
using onlineCinema.Domain.Entities;
using onlineCinema.Infrastructure.Data;

namespace onlineCinema.Infrastructure.Repositories
{
    public class CostumeBookingRepository 
        : GenericRepository<CostumeBooking>, ICostumeBookingRepository
    {
        private readonly ApplicationDbContext _db;

        public CostumeBookingRepository(ApplicationDbContext db) 
            : base(db)
        {
            _db = db;
        }
      
        public async Task<CostumeBooking?> GetByIdWithDetailsAsync(int id)
        {
            return await _db.CostumeBookings
                .Include(b => b.AttendanceLogs)
                    .ThenInclude(t => t.Inventary) 
                .Include(b => b.MerchOrders)
                    .ThenInclude(sb => sb.StudioMerch) 
                .FirstOrDefaultAsync(b => b.BookingId == id);
        }
       
        public async Task UpdateWithDetailsAsync(CostumeBooking newBookingData)
        {
            var existingBooking = await _db.CostumeBookings
                .Include(b => b.AttendanceLogs)
                .Include(b => b.MerchOrders)
                .FirstOrDefaultAsync(b => b.BookingId == newBookingData.BookingId);

            if (existingBooking == null) return;
           
            existingBooking.PaymentId = newBookingData.PaymentId;
          
            foreach (var existingOrder in existingBooking.MerchOrders.ToList())
            {
                if (!newBookingData.MerchOrders
                    .Any(ns => ns.ProductId == existingOrder.ProductId))
                {
                    _db.Entry(existingOrder).State = EntityState.Deleted; 
                }
            }
           
            foreach (var newOrder in newBookingData.MerchOrders)
            {
                var existingOrder = existingBooking.MerchOrders
                    .FirstOrDefault(s => s.ProductId == newOrder.ProductId);

                if (existingOrder != null)
                {
                   
                    existingOrder.Quantity = newOrder.Quantity;
                }
                else
                {
                  
                    newOrder.BookingId = existingBooking.BookingId;
                    existingBooking.MerchOrders.Add(newOrder);
                }
            }

            foreach (var existingLog in existingBooking.AttendanceLogs.ToList())
            {
                
                if (!newBookingData.AttendanceLogs
                    .Any(nt => nt.ItemId == existingLog.ItemId))
                {
                    _db.Entry(existingLog).State = EntityState.Deleted;
                }
            }

            foreach (var newLog in newBookingData.AttendanceLogs)
            {
                var existingLog = existingBooking.AttendanceLogs
                    .FirstOrDefault(t => t.ItemId == newLog.ItemId);

                if (existingLog == null)
                {
                    newLog.BookingId = existingBooking.BookingId;
                    newLog.ClassId = existingBooking.AttendanceLogs
                        .FirstOrDefault()?.ClassId ?? newLog.ClassId; 
                    existingBooking.AttendanceLogs.Add(newLog);
                }
            }
        }

        public async Task<(
            IEnumerable<CostumeBooking> Items, 
            int TotalCount, 
            bool HasNext, 
            bool HasPrevious)> 
            GetUserBookingsSeekAsync(
                string userId, 
                int? lastId, 
                int? firstId, 
                int pageSize)
        {
            var query = _db.CostumeBookings.Where(b => b.ApplicationUserId == userId);
            int totalCount = await query.CountAsync();

            IQueryable<CostumeBooking> dataQuery = query;

            if (lastId.HasValue)
            {
                dataQuery = dataQuery
                    .Where(b => b.BookingId < lastId.Value)
                    .OrderByDescending(b => b.BookingId);
            }
            else if (firstId.HasValue)
            {
                dataQuery = dataQuery
                    .Where(b => b.BookingId > firstId.Value)
                    .OrderBy(b => b.BookingId);
            }
            else
            {
                dataQuery = dataQuery.OrderByDescending(b => b.BookingId);
            }

            var items = await dataQuery
                .Include(b => b.FinancialTransaction)
                .Include(b => b.AttendanceLogs)
                    .ThenInclude(t => t.Inventary)
                .Include(b => b.AttendanceLogs)
                    .ThenInclude(t => t.DanceClass)
                        .ThenInclude(s => s.Performance)
                .Include(b => b.AttendanceLogs)
                    .ThenInclude(t => t.DanceClass)
                        .ThenInclude(s => s.DancerHall)
                .Include(b => b.MerchOrders)
                    .ThenInclude(sb => sb.StudioMerch)
                .Take(pageSize)
                .ToListAsync();

            if (firstId.HasValue)
            {
                items.Reverse();
            }

            bool hasNext = false;
            bool hasPrevious = false;

            if (items.Any())
            {
                var currentMaxId = items.First().BookingId;
                var currentMinId = items.Last().BookingId;

                hasNext = await _db.CostumeBookings
                    .AnyAsync(b => b.ApplicationUserId == userId 
                        && b.BookingId < currentMinId);

                hasPrevious = await _db.CostumeBookings
                    .AnyAsync(b => b.ApplicationUserId == userId 
                        && b.BookingId > currentMaxId);
            }

            return (items, totalCount, hasNext, hasPrevious);
        }
        public async Task<IEnumerable<CostumeBooking>> 
            GetUserBookingsWithDetailsAsync(string userId)
        {
            return await _db.CostumeBookings
                .Where(b => b.ApplicationUserId == userId)
                .Include(b => b.FinancialTransaction)
                .Include(b => b.AttendanceLogs)
                    .ThenInclude(t => t.Inventary)
                .Include(b => b.AttendanceLogs)
                    .ThenInclude(t => t.DanceClass)
                        .ThenInclude(s => s.Performance)
                .Include(b => b.AttendanceLogs)
                    .ThenInclude(t => t.DanceClass)
                        .ThenInclude(s => s.DancerHall)
                .Include(b => b.MerchOrders)
                    .ThenInclude(sb => sb.StudioMerch)
                .OrderByDescending(b => b.BookingId)
                .ToListAsync();
        }
    }
}
