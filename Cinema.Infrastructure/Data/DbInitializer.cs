using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using onlineCinema.Domain.Entities;
using onlineCinema.Domain.Enums;

namespace onlineCinema.Infrastructure.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            //context.Tickets.RemoveRange(context.Tickets);
            //context.Bookings.RemoveRange(context.Bookings);
            //context.Sessions.RemoveRange(context.Sessions);
            //context.Seats.RemoveRange(context.Seats);
            //context.Halls.RemoveRange(context.Halls);
            //context.Cinemas.RemoveRange(context.Cinemas);
            //context.Movies.RemoveRange(context.Movies);
            //context.Snacks.RemoveRange(context.Snacks);

            if (!context.Features.Any())
            {
                context.Features.AddRange(new List<Feature>
                {
                    new Feature { Name = "IMAX", Description = "Величезний екран та кришталево чисте зображення" },
                    new Feature { Name = "Dolby Atmos", Description = "Революційна система об'ємного звуку" },
                    new Feature { Name = "3D", Description = "Підтримка перегляду в 3D окулярах" },
                    new Feature { Name = "4DX", Description = "Рухомі крісла та ефекти навколишнього середовища" },
                    new Feature { Name = "VIP-крісла", Description = "Крісла-реклайнери з підвищеним комфортом" },
                    new Feature { Name = "Laser", Description = "Лазерна проекція високої чіткості" }
                });
                await context.SaveChangesAsync();
            }

            // Видаляємо тестового користувача, якщо він існує
            var existingUser = await userManager.FindByEmailAsync("test@user.com");
            if (existingUser != null)
            {
                await userManager.DeleteAsync(existingUser);
            }

            await context.SaveChangesAsync();
            if (!context.Snacks.Any())
            {
                context.Snacks.AddRange(new List<Snack>
                {
                    new Snack { SnackName = "Попкорн Солоний (L)", Price = 150.00m },
                    new Snack { SnackName = "Попкорн Сирний (M)", Price = 120.00m },
                    new Snack { SnackName = "Coca-Cola 0.5", Price = 60.00m },
                    new Snack { SnackName = "Nachos з сиром", Price = 140.00m }
                });
                await context.SaveChangesAsync();
            }

            if (context.Cinemas.Any())
            {
                return;
            }

            // 2. Створюємо Кінотеатр
            var cinema = new Cinema
            {
                CinemaName = "Київська Русь",
                City = "Київ",
                Street = "Січових Стрільців",
                Building = 93,
                TimeOpen = new TimeSpan(9, 0, 0),
                TimeClose = new TimeSpan(23, 0, 0)
            };
            context.Cinemas.Add(cinema);
            await context.SaveChangesAsync();

            // 3. Створюємо Зал
            var hall = new Hall
            {
                CinemaId = cinema.CinemaId,
                HallNumber = 1,
                RowCount = 5,
                SeatInRowCount = 8
            };
            context.Halls.Add(hall);
            await context.SaveChangesAsync();

            // 4. ГЕНЕРУЄМО МІСЦЯ (Найважливіше!)
            // Робимо 5 рядів по 8 місць
            var seats = new List<Seat>();
            for (byte row = 1; row <= hall.RowCount; row++)
            {
                for (byte number = 1; number <= hall.SeatInRowCount; number++)
                {
                    var seat = new Seat
                    {
                        HallId = hall.HallId,
                        RowNumber = row,
                        SeatNumber = number,
                        Type = SeatType.Standard,
                        Coefficient = 1.0f
                    };

                    // Зробимо 5-й ряд VIP місцями
                    if (row == 5)
                    {
                        seat.Type = SeatType.VIP;
                        seat.Coefficient = 1.5f;
                    }

                    seats.Add(seat);
                }
            }
            context.Seats.AddRange(seats);
            await context.SaveChangesAsync();

            // 5. Створюємо Фільм
            var movie = new Movie
            {
                Title = "Дюна: Частина Друга",
                Description = "Пол Атрід об'єднується з Чані та фрименами...",
                Runtime = 166,
                ReleaseDate = DateTime.Now.AddDays(-10),
                Status = MovieStatus.Released,
                AgeRating = AgeRating.Age12
            };
            context.Movies.Add(movie);
            await context.SaveChangesAsync();

            // 6. Створюємо Сеанс (На завтра)
            var session = new Session
            {
                MovieId = movie.Id,
                HallId = hall.HallId,
                ShowingDateTime = DateTime.Now.AddDays(10).Date.AddHours(19), // Завтра о 19:00
                BasePrice = 200.00m
            };
            context.Sessions.Add(session);
            await context.SaveChangesAsync();

            // 7. Створюємо Тестового Юзера (для імітації зайнятих місць)
            var user1 = new ApplicationUser
            {
                UserName = "user1@test.com",
                Email = "user1@test.com",
                FirstName = "Користувач",
                LastName = "Один",
                DateOfBirth = new DateTime(1990, 1, 1),
                EmailConfirmed = true
            };
            await userManager.CreateAsync(user1, "Pa$$w0rd");

            // Користувач 2
            var user2 = new ApplicationUser
            {
                UserName = "user2@test.com",
                Email = "user2@test.com",
                FirstName = "Користувач",
                LastName = "Два",
                DateOfBirth = new DateTime(1995, 1, 1),
                EmailConfirmed = true
            };
            await userManager.CreateAsync(user2, "Pa$$w0rd");

            // 8. Створюємо Бронь (Імітуємо, що хтось вже купив квитки)
            var booking = new Booking
            {
                ApplicationUserId = user1.Id,
                CreatedDateTime = DateTime.Now,
                EmailAddress = user1.Email
            };
            context.Bookings.Add(booking);
            await context.SaveChangesAsync();

            // Займемо 1 ряд, місця 3 і 4
            var bookedSeats = seats.Where(s => s.RowNumber == 1 && (s.SeatNumber == 3 || s.SeatNumber == 4)).ToList();

            foreach (var seat in bookedSeats)
            {
                context.Tickets.Add(new Ticket
                {
                    SessionId = session.SessionId,
                    SeatId = seat.SeatId,
                    BookingId = booking.BookingId,
                    Price = session.BasePrice // Ціна квитка
                });
            }
            await context.SaveChangesAsync();
        }
    }
}
