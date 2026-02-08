using Microsoft.EntityFrameworkCore;
using onlineCinema.Application.Interfaces;
using onlineCinema.Domain.Entities;
using onlineCinema.Infrastructure.Data;

namespace onlineCinema.Infrastructure.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        private readonly ApplicationDbContext _db;

        public BookingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
      
        public async Task<Booking?> GetByIdWithDetailsAsync(int id)
        {
            return await _db.Bookings
                .Include(b => b.Tickets)
                    .ThenInclude(t => t.Seat) 
                .Include(b => b.SnackBookings)
                    .ThenInclude(sb => sb.Snack) 
                .FirstOrDefaultAsync(b => b.BookingId == id);
        }
       
        public async Task UpdateWithDetailsAsync(Booking newBookingData)
        {
            var existingBooking = await _db.Bookings
                .Include(b => b.Tickets)
                .Include(b => b.SnackBookings)
                .FirstOrDefaultAsync(b => b.BookingId == newBookingData.BookingId);

            if (existingBooking == null) return;
           
            existingBooking.PaymentId = newBookingData.PaymentId;
          
            foreach (var existingSnack in existingBooking.SnackBookings.ToList())
            {
                if (!newBookingData.SnackBookings.Any(ns => ns.SnackId == existingSnack.SnackId))
                {
                    _db.Entry(existingSnack).State = EntityState.Deleted; 
                }
            }
           
            foreach (var newSnack in newBookingData.SnackBookings)
            {
                var existingSnack = existingBooking.SnackBookings
                    .FirstOrDefault(s => s.SnackId == newSnack.SnackId);

                if (existingSnack != null)
                {
                   
                    existingSnack.Quantity = newSnack.Quantity;
                }
                else
                {
                  
                    newSnack.BookingId = existingBooking.BookingId;
                    existingBooking.SnackBookings.Add(newSnack);
                }
            }

            foreach (var existingTicket in existingBooking.Tickets.ToList())
            {
                
                if (!newBookingData.Tickets.Any(nt => nt.SeatId == existingTicket.SeatId))
                {
                    _db.Entry(existingTicket).State = EntityState.Deleted;
                }
            }

            foreach (var newTicket in newBookingData.Tickets)
            {
                var existingTicket = existingBooking.Tickets
                    .FirstOrDefault(t => t.SeatId == newTicket.SeatId);

                if (existingTicket == null)
                {
                    newTicket.BookingId = existingBooking.BookingId;
                    newTicket.SessionId = existingBooking.Tickets
                        .FirstOrDefault()?.SessionId ?? newTicket.SessionId; 
                    existingBooking.Tickets.Add(newTicket);
                }
            }
        }

        public async Task<IEnumerable<Booking>> GetUserBookingsWithDetailsAsync(string userId)
        {
            return await _db.Bookings
                .Where(b => b.ApplicationUserId == userId)
                .Include(b => b.Payment)
                .Include(b => b.Tickets)
                    .ThenInclude(t => t.Seat)
                .Include(b => b.Tickets)
                    .ThenInclude(t => t.Session)
                        .ThenInclude(s => s.Movie)
                .Include(b => b.Tickets)
                    .ThenInclude(t => t.Session)
                        .ThenInclude(s => s.Hall)
                // Завантажуємо снеки
                .Include(b => b.SnackBookings)
                    .ThenInclude(sb => sb.Snack)

                .OrderByDescending(b => b.CreatedDateTime)
                .ToListAsync();
        }
    }
}
