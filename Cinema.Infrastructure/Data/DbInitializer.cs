using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using onlineCinema.Domain.Entities;
using onlineCinema.Domain.Enums;

namespace onlineCinema.Infrastructure.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            // --- 1. ОЧИЩЕННЯ БАЗИ ДАНИХ (Твій перевірений метод) ---
            await context.Database.ExecuteSqlRawAsync("EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'");

            try
            {
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [Tickets]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [SnackBooking]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [Bookings]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [Payments]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [Sessions]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [Seats]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [MovieGenre]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [MovieCast]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [DirectorMovie]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [LanguageMovie]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [MovieFeature]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [HallFeature]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [Movies]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [Halls]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [Cinemas]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [Snacks]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [Features]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [Genres]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [CastMembers]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [Directors]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [Languages]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [AspNetUsers]");

                await context.Database.ExecuteSqlRawAsync("EXEC sp_MSforeachtable 'IF OBJECTPROPERTY(OBJECT_ID(''?''), ''TableHasIdentity'') = 1 DBCC CHECKIDENT (''?'', RESEED, 0)'");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при очищенні: {ex.Message}");
            }
            finally
            {
                await context.Database.ExecuteSqlRawAsync("EXEC sp_MSforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all'");
            }

            // --- 2. ЗАПОВНЕННЯ ДАНИМИ ---

            var features = new List<Feature>
            {
                new Feature { Name = "IMAX", Description = "Величезний екран" },
                new Feature { Name = "Dolby Atmos", Description = "Об'ємний звук" },
                new Feature { Name = "VIP-крісла", Description = "Реклайнери" }
            };
            await context.Features.AddRangeAsync(features);

            await context.Snacks.AddRangeAsync(new List<Snack>
            {
                new Snack { SnackName = "Попкорн Солоний (L)", Price = 150.00m },
                new Snack { SnackName = "Coca-Cola 0.5", Price = 60.00m },
                new Snack { SnackName = "Nachos з сиром", Price = 140.00m }
            });

            var genres = new List<Genre> { new Genre { GenreName = "Science Fiction" }, new Genre { GenreName = "Action" }, new Genre { GenreName = "Biography" } };
            await context.Genres.AddRangeAsync(genres);

            var directors = new List<Director> { new Director { DirectorFirstName = "Denis", DirectorLastName = "Villeneuve" }, new Director { DirectorFirstName = "Christopher", DirectorLastName = "Nolan" } };
            await context.Directors.AddRangeAsync(directors);

            var cast = new List<CastMember> { new CastMember { CastFirstName = "Timothée", CastLastName = "Chalamet" }, new CastMember { CastFirstName = "Cillian", CastLastName = "Murphy" } };
            await context.CastMembers.AddRangeAsync(cast);

            await context.SaveChangesAsync();

            // --- 3. ФІЛЬМИ ---

            var duneMovie = new Movie
            {
                Title = "Дюна: Частина друга",
                Status = MovieStatus.Released,
                AgeRating = AgeRating.Age12,
                Runtime = 166,
                ReleaseDate = new DateTime(2024, 2, 29),
                TrailerLink = "https://www.youtube.com/watch?v=Way9Dexny3w",
                Description = "Пол Атрід об'єднується з Чані та фрименами...",
                PosterImage = "/images/movies/dune2.jpg"
            };
            var oppenheimerMovie = new Movie
            {
                Title = "Оппенгеймер",
                Status = MovieStatus.Released,
                AgeRating = AgeRating.Age16,
                Runtime = 180,
                ReleaseDate = new DateTime(2023, 7, 20),
                TrailerLink = "https://www.youtube.com/watch?v=uYPbbksJxIg",
                Description = "Історія створення атомної бомби.",
                PosterImage = "/images/movies/oppenheimer.jpg"
            };
            await context.Movies.AddRangeAsync(duneMovie, oppenheimerMovie);
            await context.SaveChangesAsync();

            // Зв'язки
            duneMovie.MovieGenres.Add(new MovieGenre { GenreId = genres[0].GenreId });
            oppenheimerMovie.MovieGenres.Add(new MovieGenre { GenreId = genres[2].GenreId });

            // --- 4. КІНОТЕАТР ТА ЗАЛ ---

            var cinema = new Cinema { CinemaName = "CinePrime Plaza", City = "Kyiv", Street = "Khreshchatyk", Building = 22, TimeOpen = new TimeSpan(9, 0, 0), TimeClose = new TimeSpan(23, 59, 0) };
            context.Cinemas.Add(cinema);
            await context.SaveChangesAsync();

            var hall = new Hall { CinemaId = cinema.CinemaId, HallNumber = 1, RowCount = 8, SeatInRowCount = 12 };
            context.Halls.Add(hall);
            await context.SaveChangesAsync();

            // Місця
            var seats = new List<Seat>();
            for (byte r = 1; r <= hall.RowCount; r++)
                for (byte n = 1; n <= hall.SeatInRowCount; n++)
                    seats.Add(new Seat { HallId = hall.HallId, RowNumber = r, SeatNumber = n, Type = r > 6 ? SeatType.VIP : SeatType.Standard, Coefficient = r > 6 ? 1.5f : 1.0f });

            context.Seats.AddRange(seats);
            await context.SaveChangesAsync();

            // --- 5. ГЕНЕРАЦІЯ БАГАТЬОХ СЕАНСІВ (на 7 днів) ---

            var sessions = new List<Session>();
            var today = DateTime.Now.Date;

            for (int i = 0; i < 7; i++)
            {
                var date = today.AddDays(i);

                // Сеанси для Дюни
                sessions.Add(new Session { MovieId = duneMovie.Id, HallId = hall.HallId, ShowingDateTime = date.AddHours(10).AddMinutes(0), BasePrice = 160 });
                sessions.Add(new Session { MovieId = duneMovie.Id, HallId = hall.HallId, ShowingDateTime = date.AddHours(14).AddMinutes(30), BasePrice = 190 });
                sessions.Add(new Session { MovieId = duneMovie.Id, HallId = hall.HallId, ShowingDateTime = date.AddHours(19).AddMinutes(0), BasePrice = 220 });
                sessions.Add(new Session { MovieId = duneMovie.Id, HallId = hall.HallId, ShowingDateTime = date.AddHours(22).AddMinutes(15), BasePrice = 180 });

                // Сеанси для Оппенгеймера
                sessions.Add(new Session { MovieId = oppenheimerMovie.Id, HallId = hall.HallId, ShowingDateTime = date.AddHours(12).AddMinutes(0), BasePrice = 150 });
                sessions.Add(new Session { MovieId = oppenheimerMovie.Id, HallId = hall.HallId, ShowingDateTime = date.AddHours(17).AddMinutes(0), BasePrice = 200 });
                sessions.Add(new Session { MovieId = oppenheimerMovie.Id, HallId = hall.HallId, ShowingDateTime = date.AddHours(21).AddMinutes(45), BasePrice = 210 });
            }

            await context.Sessions.AddRangeAsync(sessions);
            await context.SaveChangesAsync();

            // --- 6. КОРИСТУВАЧІ ТА ТЕСТОВА БРОНЬ ---

            var user1 = new ApplicationUser { UserName = "user1@test.com", Email = "user1@test.com", FirstName = "Ivan", LastName = "User", EmailConfirmed = true, DateOfBirth = new DateTime(1995, 1, 1) };
            await userManager.CreateAsync(user1, "Pa$$w0rd");

            var booking = new Booking { ApplicationUserId = user1.Id, CreatedDateTime = DateTime.Now, EmailAddress = user1.Email };
            await context.Bookings.AddAsync(booking);
            await context.SaveChangesAsync();

            // Бронюємо місця на перший сеанс Дюни сьогодні
            var targetSession = sessions.First(s => s.MovieId == duneMovie.Id);
            var bookedSeats = seats.Where(s => s.RowNumber == 4 && (s.SeatNumber == 5 || s.SeatNumber == 6)).ToList();
            foreach (var s in bookedSeats)
                context.Tickets.Add(new Ticket { SessionId = targetSession.SessionId, SeatId = s.SeatId, BookingId = booking.BookingId, Price = targetSession.BasePrice });

            await context.SaveChangesAsync();
        }
    }
}