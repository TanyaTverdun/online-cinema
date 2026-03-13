using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using onlineCinema.Domain.Constants;
using onlineCinema.Domain.Entities;
using onlineCinema.Domain.Enums;
using System.Text.Json;
using System.IO;

namespace onlineCinema.Infrastructure.Data
{
    public class DbInitializer
    {
        private static async Task<List<T>> LoadDataFromJson<T>(string filePath)
        {
            var basePath = AppContext.BaseDirectory;
            var fullPath = Path.Combine(basePath, filePath);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"Файл не знайдено: {fullPath}");

            var json = await File.ReadAllTextAsync(fullPath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<List<T>>(json, options) ?? new List<T>();
        }

        public static async Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
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

                await context.Database.ExecuteSqlRawAsync("DELETE FROM [AspNetUserRoles]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [AspNetUserClaims]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [AspNetUserLogins]");
                await context.Database.ExecuteSqlRawAsync("DELETE FROM [AspNetUserTokens]");

                await context.Database.ExecuteSqlRawAsync("DELETE FROM [AspNetUsers]");

                await context.Database.ExecuteSqlRawAsync("EXEC sp_MSforeachtable 'IF OBJECTPROPERTY(OBJECT_ID(''?''), ''TableHasIdentity'') = 1 DBCC CHECKIDENT (''?'', RESEED, 0)'");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при очищенні: {ex.Message}");
                throw;
            }
            finally
            {
                await context.Database.ExecuteSqlRawAsync("EXEC sp_MSforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all'");
            }

            var actors = await LoadDataFromJson<CastMember>("Data/SeedData/actors.json");
            await context.CastMembers.AddRangeAsync(actors);
            await SaveChangesWithIdentityInsert(context, "CastMembers");

            var directors = await LoadDataFromJson<Director>("Data/SeedData/directors.json");
            await context.Directors.AddRangeAsync(directors);
            await SaveChangesWithIdentityInsert(context, "Directors");

            var languages = await LoadDataFromJson<Language>("Data/SeedData/languages.json");
            await context.Languages.AddRangeAsync(languages);
            await SaveChangesWithIdentityInsert(context, "Languages");

            var features = await LoadDataFromJson<Feature>("Data/SeedData/features.json");
            await context.Features.AddRangeAsync(features);
            await SaveChangesWithIdentityInsert(context, "Features");

            var snacks = await LoadDataFromJson<Snack>("Data/SeedData/snacks.json");
            await context.Snacks.AddRangeAsync(snacks);
            await SaveChangesWithIdentityInsert(context, "Snacks");

            var genres = await LoadDataFromJson<Genre>("Data/SeedData/genres.json");
            await context.Genres.AddRangeAsync(genres);
            await SaveChangesWithIdentityInsert(context, "Genres");

            // фільми
            await context.Database.OpenConnectionAsync();
            try
            {
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Movies ON");
                var movies = new List<Movie>
            {
                new() {
                    Id = 1,
                    Title = "Дюна: Частина друга",
                    Status = MovieStatus.Released,
                    AgeRating = AgeRating.Age13,
                    Rating = 8.5m,
                    Runtime = 166,
                    ReleaseDate = new DateTime(2024, 2, 29),
                    TrailerLink = "https://www.youtube.com/watch?v=Ljzu52GMytk",
                    PosterImage = "https://res.cloudinary.com/dkesgu722/image/upload/v1773425719/dune2_dhvsla.jpg",
                    Description = "Пол Атрід об'єднується з Чані та фріменами, щоб помститися заколотникам, які знищили його родину. Перед ним постає вибір між коханням усього життя та долею відомого всесвіту.",
                    MovieDirectors = new List<DirectorMovie> { new() { DirectorId = 1 } },
                    MovieLanguages = new List<LanguageMovie> { new() { LanguageId = 1 }, new() { LanguageId = 2 } },
                    MovieFeatures = new List<MovieFeature> { new() { FeatureId = 6 }, new() { FeatureId = 1 }, new() { FeatureId = 3 } },
                    MovieCasts = new List<MovieCast> { new() { CastId = 1 }, new() { CastId = 2 } },
                    MovieGenres = new List<MovieGenre> { new() { GenreId = 1 }, new() { GenreId = 7 } }
                },
                new() {
                    Id = 2,
                    Title = "Оппенгеймер",
                    Status = MovieStatus.Released,
                    AgeRating = AgeRating.Age16,
                    Rating = 7.9m,
                    Runtime = 180,
                    ReleaseDate = new DateTime(2023, 7, 20),
                    TrailerLink = "https://www.youtube.com/watch?v=eZYD5NRum4A",
                    PosterImage = "https://res.cloudinary.com/dkesgu722/image/upload/v1773425726/oppenheimer_xf6y6p.jpg",
                    Description = "Історія життя американського фізика Роберта Оппенгеймера, який очолив секретну розробку атомної бомби під час Другої світової війни.",
                    MovieDirectors = new List<DirectorMovie> { new() { DirectorId = 2 } },
                    MovieLanguages = new List<LanguageMovie> { new() { LanguageId = 1 }, new() { LanguageId = 2 } },
                    MovieFeatures = new List<MovieFeature> { new() { FeatureId = 2 }, new() { FeatureId = 5 }, new() { FeatureId = 3 } },
                    MovieCasts = new List<MovieCast> { new() { CastId = 3 } },
                    MovieGenres = new List<MovieGenre> { new() { GenreId = 2 } }
                },
                new() {
                    Id = 3,
                    Title = "Думками навиворіт 2",
                    Status = MovieStatus.Released,
                    AgeRating = AgeRating.Age0,
                    Rating = 9.2m,
                    Runtime = 96,
                    ReleaseDate = DateTime.Now.AddDays(-60),
                    TrailerLink = "https://www.youtube.com/watch?v=QSmqzCMe90M",
                    PosterImage = "https://res.cloudinary.com/dkesgu722/image/upload/v1773425721/insideout2_g9aqme.jpg",
                    Description = "Райлі стає підлітком, і в її головному відділі з'являються нові емоції, зокрема Тривожність, які намагаються витіснити старих друзів.",
                    MovieDirectors = new List<DirectorMovie> { new() { DirectorId = 13 } },
                    MovieLanguages = new List<LanguageMovie> { new() { LanguageId = 1 } },
                    MovieFeatures = new List<MovieFeature> { new() { FeatureId = 2 }, new() { FeatureId = 1 } },
                    MovieCasts = new List<MovieCast> { new() { CastId = 15 } },
                    MovieGenres = new List<MovieGenre> { new() { GenreId = 5 }, new() { GenreId = 4 } }
                },
                new() {
                    Id = 4,
                    Title = "Дедпул і Росомаха",
                    Status = MovieStatus.Released,
                    AgeRating = AgeRating.Age18,
                    Rating = 7.5m,
                    Runtime = 127,
                    ReleaseDate = DateTime.Now.AddDays(-30),
                    TrailerLink = "https://www.youtube.com/watch?v=ACGBv-CZ3bM",
                    PosterImage = "https://res.cloudinary.com/dkesgu722/image/upload/v1773425719/deadpool3_qj9fqj.jpg",
                    Description = "Уейд Вілсон намагається жити спокійним життям, але коли його світу загрожує небезпека, він змушений звернутися по допомогу до дуже неохочого Росомахи.",
                    MovieDirectors = new List<DirectorMovie> { new() { DirectorId = 3 } },
                    MovieLanguages = new List<LanguageMovie> { new() { LanguageId = 1 }, new() { LanguageId = 2 } },
                    MovieFeatures = new List<MovieFeature> { new() { FeatureId = 4 }, new() { FeatureId = 3 } },
                    MovieCasts = new List<MovieCast> { new() { CastId = 4 }, new() { CastId = 5 } },
                    MovieGenres = new List<MovieGenre> { new() { GenreId = 3 }, new() { GenreId = 4 } }
                },
                new() {
                    Id = 5,
                    Title = "Джокер: Божевілля на двох",
                    Status = MovieStatus.Released,
                    AgeRating = AgeRating.Age18,
                    Runtime = 138,
                    Rating = 8.0m,
                    ReleaseDate = DateTime.Now.AddDays(-10),
                    TrailerLink = "https://www.youtube.com/watch?v=0-wp52fQ6gI",
                    PosterImage = "https://res.cloudinary.com/dkesgu722/image/upload/v1773425722/joker2_aregdk.jpg",
                    Description = "Артур Флек перебуває у лікарні Аркгем, очікуючи на суд. Там він зустрічає кохання свого життя — Харлі Квінн, і разом вони занурюються у спільне божевілля.",
                    MovieDirectors = new List<DirectorMovie> { new() { DirectorId = 4 } },
                    MovieLanguages = new List<LanguageMovie> { new() { LanguageId = 1 }, new() { LanguageId = 2 } },
                    MovieFeatures = new List<MovieFeature> { new() { FeatureId = 2 }, new() { FeatureId = 3 } },
                    MovieCasts = new List<MovieCast> { new() { CastId = 6 }, new() { CastId = 7 } },
                    MovieGenres = new List<MovieGenre> { new() { GenreId = 2 } }
                },
                new() {
                    Id = 6,
                    Title = "Гладіатор II",
                    Status = MovieStatus.Released,
                    AgeRating = AgeRating.Age16,
                    Rating = 7.8m,
                    Runtime = 148,
                    ReleaseDate = DateTime.Now.AddDays(-5),
                    TrailerLink = "https://www.youtube.com/watch?v=7K8V33m5vMU",
                    PosterImage = "https://res.cloudinary.com/dkesgu722/image/upload/v1773425720/gladiator2_xjis3h.jpg",
                    Description = "Через роки після смерті Максимуса, Луцій змушений вийти на арену Колізею, щоб знайти силу та честь і повернути славу Риму.",
                    MovieDirectors = new List<DirectorMovie> { new() { DirectorId = 5 } },
                    MovieLanguages = new List<LanguageMovie> { new() { LanguageId = 1 } },
                    MovieFeatures = new List<MovieFeature> { new() { FeatureId = 6 }, new() { FeatureId = 2 } },
                    MovieCasts = new List<MovieCast> { new() { CastId = 9 }, new() { CastId = 8 } },
                    MovieGenres = new List<MovieGenre> { new() { GenreId = 3 }, new() { GenreId = 2 } }
                },
                new() {
                    Id = 7,
                    Title = "Аватар: Вогонь і попіл",
                    Status = MovieStatus.ComingSoon,
                    AgeRating = AgeRating.Age13,
                    Rating = 8.3m,
                    Runtime = 190,
                    ReleaseDate = DateTime.Now.AddMonths(12),
                    TrailerLink = "https://www.youtube.com/watch?v=os_CcXsSHPM",
                    PosterImage = "https://res.cloudinary.com/dkesgu722/image/upload/v1773425718/avatar3_m0kjyp.jpg",
                    Description = "Джейк Саллі та Нейтірі стикаються з новим агресивним племенем На'ві — 'Людьми Попелу', які показують темну сторону Пандори.",
                    MovieDirectors = new List<DirectorMovie> { new() { DirectorId = 6 } },
                    MovieLanguages = new List<LanguageMovie> { new() { LanguageId = 1 }, new() { LanguageId = 2 } },
                    MovieFeatures = new List<MovieFeature> { new() { FeatureId = 6 }, new() { FeatureId = 1 }, new() { FeatureId = 4 } },
                    MovieCasts = new List<MovieCast> { new() { CastId = 15 } },
                    MovieGenres = new List<MovieGenre> { new() { GenreId = 1 }, new() { GenreId = 7 } }
                },
                new() {
                    Id = 8,
                    Title = "Бетмен: Частина 2",
                    Status = MovieStatus.ComingSoon,
                    AgeRating = AgeRating.Age16,
                    Rating = 7.1m,
                    Runtime = 160,
                    ReleaseDate = DateTime.Now.AddMonths(18),
                    TrailerLink = "https://www.youtube.com/watch?v=zC6-H-EcEmU",
                    PosterImage = "https://res.cloudinary.com/dkesgu722/image/upload/v1773425718/batman2_lkqtme.jpg",
                    Description = "Продовження історії Брюса Вейна, який намагається навести лад у Готемі, стикаючись з новими загрозами, що виходять з найтемніших куточків міста.",
                    MovieDirectors = new List<DirectorMovie> { new() { DirectorId = 7 } },
                    MovieLanguages = new List<LanguageMovie> { new() { LanguageId = 1 }, new() { LanguageId = 2 } },
                    MovieFeatures = new List<MovieFeature> { new() { FeatureId = 2 }, new() { FeatureId = 3 } },
                    MovieCasts = new List<MovieCast> { new() { CastId = 10 } },
                    MovieGenres = new List<MovieGenre> { new() { GenreId = 6 }, new() { GenreId = 3 } }
                },
                new() {
                    Id = 9,
                    Title = "Сонік 3",
                    Status = MovieStatus.Released,
                    AgeRating = AgeRating.Age6,
                    Rating = 7.0m,
                    Runtime = 110,
                    ReleaseDate = DateTime.Now.AddDays(-2),
                    TrailerLink = "https://www.youtube.com/watch?v=bDIUaKYV_Tg",
                    PosterImage = "https://res.cloudinary.com/dkesgu722/image/upload/v1773425728/sonic3_hkooyq.jpg",
                    Description = "Сонік, Тейлз і Наклз повинні об'єднатися, щоб зупинити нового могутнього супротивника — їжака Шедоу, чиє минуле оповите таємницею.",
                    MovieDirectors = new List<DirectorMovie> { new() { DirectorId = 8 } },
                    MovieLanguages = new List<LanguageMovie> { new() { LanguageId = 1 }, new() { LanguageId = 4 } },
                    MovieFeatures = new List<MovieFeature> { new() { FeatureId = 1 }, new() { FeatureId = 2 } },
                    MovieCasts = new List<MovieCast> { new() { CastId = 15 } },
                    MovieGenres = new List<MovieGenre> { new() { GenreId = 5 }, new() { GenreId = 4 } }
                },
                new() {
                    Id = 10,
                    Title = "Супермен",
                    Status = MovieStatus.ComingSoon,
                    AgeRating = AgeRating.Age13,
                    Rating = 7.6m,
                    Runtime = 150,
                    ReleaseDate = DateTime.Now.AddMonths(6),
                    TrailerLink = "https://www.youtube.com/watch?v=ALfxbq2RhXw",
                    PosterImage = "https://res.cloudinary.com/dkesgu722/image/upload/v1773425729/superman_ekhg8t.jpg",
                    Description = "Нове бачення історії Кларка Кента, який намагається примирити свою криптонську спадщину з людським вихованням у маленькому містечку.",
                    MovieDirectors = new List<DirectorMovie> { new() { DirectorId = 9 } },
                    MovieLanguages = new List<LanguageMovie> { new() { LanguageId = 1 }, new() { LanguageId = 2 } },
                    MovieFeatures = new List<MovieFeature> { new() { FeatureId = 6 }, new() { FeatureId = 2 } },
                    MovieCasts = new List<MovieCast> { new() { CastId = 11 } }, // Умовно Том Круз як камео або інший актор
                    MovieGenres = new List<MovieGenre> { new() { GenreId = 1 }, new() { GenreId = 3 } }
                },
                new() {
                    Id = 11,
                    Title = "Носферату",
                    Status = MovieStatus.Released,
                    AgeRating = AgeRating.Age18,
                    Rating = 8.4m,
                    Runtime = 132,
                    ReleaseDate = DateTime.Now.AddDays(-3),
                    TrailerLink = "https://www.youtube.com/watch?v=ggvZEjSftvw",
                    PosterImage = "https://res.cloudinary.com/dkesgu722/image/upload/v1773425724/nosferatu_pm8lv6.jpg",
                    Description = "Готична казка про одержимість молодої жінки та страхітливого вампіра, що закохався в неї, залишаючи за собою шлейф жаху.",
                    MovieDirectors = new List<DirectorMovie> { new() { DirectorId = 10 } },
                    MovieLanguages = new List<LanguageMovie> { new() { LanguageId = 1 }, new() { LanguageId = 2 } },
                    MovieFeatures = new List<MovieFeature> { new() { FeatureId = 2 }, new() { FeatureId = 5 } },
                    MovieCasts = new List<MovieCast> { new() { CastId = 12 }, new() { CastId = 13 } },
                    MovieGenres = new List<MovieGenre> { new() { GenreId = 6 } }
                },
                new() {
                    Id = 12,
                    Title = "Ваяна 2",
                    Status = MovieStatus.Released,
                    AgeRating = AgeRating.Age0,
                    Rating = 8.7m,
                    Runtime = 100,
                    ReleaseDate = DateTime.Now.AddDays(-7),
                    TrailerLink = "https://www.youtube.com/watch?v=c40yI1R1aXA",
                    PosterImage = "https://res.cloudinary.com/dkesgu722/image/upload/v1773425724/moana2_hbarcv.jpg",
                    Description = "Ваяна отримує несподіваний виклик від своїх пращурів і вирушає у далекі моря Океанії, щоб об'єднати свій народ.",
                    MovieDirectors = new List<DirectorMovie> { new() { DirectorId = 11 } },
                    MovieLanguages = new List<LanguageMovie> { new() { LanguageId = 1 } },
                    MovieFeatures = new List<MovieFeature> { new() { FeatureId = 1 }, new() { FeatureId = 2 } },
                    MovieCasts = new List<MovieCast> { new() { CastId = 15 } },
                    MovieGenres = new List<MovieGenre> { new() { GenreId = 5 }, new() { GenreId = 7 } }
                },
                new() {
                    Id = 13,
                    Title = "Wicked: Чародійка",
                    Status = MovieStatus.Released,
                    AgeRating = AgeRating.Age13,
                    Rating = 8.1m,
                    Runtime = 160,
                    ReleaseDate = DateTime.Now.AddDays(-4),
                    TrailerLink = "https://www.youtube.com/watch?v=iid2boBVDL0",
                    PosterImage = "https://res.cloudinary.com/dkesgu722/image/upload/v1773425729/wicked_rkn1mf.jpg",
                    Description = "Неймовірна історія про те, як Ельфаба стала Злою Відьмою Заходу, а Глінда — Доброю Відьмою країни Оз.",
                    MovieDirectors = new List<DirectorMovie> { new() { DirectorId = 12 } },
                    MovieLanguages = new List<LanguageMovie> { new() { LanguageId = 1 }, new() { LanguageId = 2 } },
                    MovieFeatures = new List<MovieFeature> { new() { FeatureId = 2 }, new() { FeatureId = 3 } },
                    MovieCasts = new List<MovieCast> { new() { CastId = 7 } },
                    MovieGenres = new List<MovieGenre> { new() { GenreId = 2 }, new() { GenreId = 1 } }
                },
                new() {
                    Id = 14,
                    Title = "Пригоди Паддінгтона в Перу",
                    Status = MovieStatus.Released,
                    AgeRating = AgeRating.Age0,
                    Rating = 7.4m,
                    Runtime = 106,
                    ReleaseDate = DateTime.Now.AddDays(-1),
                    TrailerLink = "https://www.youtube.com/watch?v=Bywsl2NhjuY",
                    PosterImage = "https://res.cloudinary.com/dkesgu722/image/upload/v1773425728/paddington3_h2xirc.jpg",
                    Description = "Паддінгтон повертається до Перу, щоб відвідати свою улюблену тітоньку Люсі, але несподівана пригода веде його вглиб амазонських лісів.",
                    MovieDirectors = new List<DirectorMovie> { new() { DirectorId = 13 } },
                    MovieLanguages = new List<LanguageMovie> { new() { LanguageId = 1 }, new() { LanguageId = 4 } },
                    MovieFeatures = new List<MovieFeature> { new() { FeatureId = 2 } },
                    MovieCasts = new List<MovieCast> { new() { CastId = 12 } },
                    MovieGenres = new List<MovieGenre> { new() { GenreId = 4 }, new() { GenreId = 7 } }
                },
                new() {
                    Id = 15,
                    Title = "Погані хлопці 4",
                    Status = MovieStatus.Released,
                    AgeRating = AgeRating.Age16,
                    Rating = 9.5m,
                    Runtime = 115,
                    ReleaseDate = DateTime.Now.AddDays(-40),
                    TrailerLink = "https://www.youtube.com/watch?v=kpM2mXklu48",
                    PosterImage = "https://res.cloudinary.com/dkesgu722/image/upload/v1773425718/badboys4_gtiult.jpg",
                    Description = "Детективи Майк Лоурі та Маркус Бернетт самі стають втікачами, коли намагаються очистити ім'я свого покійного капітана.",
                    MovieDirectors = new List<DirectorMovie> { new() { DirectorId = 15 } },
                    MovieLanguages = new List<LanguageMovie> { new() { LanguageId = 1 } },
                    MovieFeatures = new List<MovieFeature> { new() { FeatureId = 4 }, new() { FeatureId = 2 } },
                    MovieCasts = new List<MovieCast> { new() { CastId = 14 } },
                    MovieGenres = new List<MovieGenre> { new() { GenreId = 3 }, new() { GenreId = 4 } }
                }
            };
                await context.Movies.AddRangeAsync(movies);
                await SaveChangesWithIdentityInsert(context, "Movies");
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Movies OFF");
            }
            finally
            {
                await context.Database.CloseConnectionAsync();
            }

            var cinemas = await LoadDataFromJson<Cinema>("Data/SeedData/cinemas.json");
            await context.Cinemas.AddRangeAsync(cinemas);
            await SaveChangesWithIdentityInsert(context, "Cinemas");

            var halls = await LoadDataFromJson<Hall>("Data/SeedData/halls.json");
            await context.Halls.AddRangeAsync(halls);
            await context.SaveChangesAsync();

            // Генерація місць
            foreach (var h in halls)
            {
                var hSeats = new List<Seat>();
                for (byte r = 1; r <= h.RowCount; r++)
                    for (byte n = 1; n <= h.SeatInRowCount; n++)
                        hSeats.Add(new Seat
                        {
                            HallId = h.HallId,
                            RowNumber = r,
                            SeatNumber = n,
                            Type = r > h.RowCount - 2 ? SeatType.VIP : SeatType.Standard,
                            Coefficient = r > h.RowCount - 2 ? 1.5f : 1.0f
                        });
                context.Seats.AddRange(hSeats);
            }
            await context.SaveChangesAsync();

            // Сеанси
            var sessions = new List<Session>();
            var startDate = DateTime.Now.Date;

            var movieToHallMap = new Dictionary<int, int>
            {
                { 1, 7 },  { 7, 10 }, { 2, 8 },  { 11, 9 }, { 3, 5 },
                { 12, 6 }, { 4, 3 },  { 5, 4 },  { 6, 1 },  { 8, 2 },
                { 9, 11 }, { 10, 12 },{ 13, 13 },{ 14, 14 },{ 15, 15 }
            };

            var startTimes = new List<TimeSpan>
            {
                new TimeSpan(10, 0, 0), new TimeSpan(13, 0, 0),
                new TimeSpan(16, 0, 0), new TimeSpan(19, 0, 0),
                new TimeSpan(22, 0, 0)
            };

            for (int day = 0; day < 8; day++)
            {
                var currentDate = startDate.AddDays(day);

                foreach (var entry in movieToHallMap)
                {
                    int movieId = entry.Key;
                    int hallId = entry.Value;

                    foreach (var time in startTimes)
                    {
                        decimal price = time.Hours >= 18 ? 250m : 180m;

                        if (hallId == 7) price += 100m;
                        else if (hallId == 8 || hallId == 9) price += 150m;

                        sessions.Add(new Session
                        {
                            MovieId = movieId,
                            HallId = hallId,
                            ShowingDateTime = currentDate.Add(time),
                            BasePrice = price
                        });
                    }
                }
            }

            await context.Sessions.AddRangeAsync(sessions);
            await context.SaveChangesAsync();

            var allFeatures = await context.Features.ToListAsync();
            var randomForFeatures = new Random();

            // Завантажуємо фільми та зали, щоб змінити їхні списки фіч
            var allMovies = await context.Movies.Include(m => m.MovieFeatures).ToListAsync();
            var allHalls = await context.Halls.Include(h => h.HallFeatures).ToListAsync();

            foreach (var entry in movieToHallMap)
            {
                int movieId = entry.Key;
                int hallId = entry.Value;

                var movie = allMovies.FirstOrDefault(m => m.Id == movieId);
                var hall = allHalls.FirstOrDefault(h => h.HallId == hallId);

                if (movie != null && hall != null)
                {
                    hall.HallFeatures.Clear();

                    // Копіюємо фічі з фільму в зал
                    foreach (var mFeature in movie.MovieFeatures)
                    {
                        hall.HallFeatures.Add(new HallFeature
                        {
                            HallId = hall.HallId,
                            FeatureId = mFeature.FeatureId
                        });
                    }

                    // 2 рандомні фічі для залу
                    var movieFeatureIds = movie.MovieFeatures.Select(mf => mf.FeatureId).ToList();
                    var extraFeatures = allFeatures
                        .Where(f => !movieFeatureIds.Contains(f.Id)) // Перевір: Id чи FeatureId у тебе в моделі Feature
                        .OrderBy(x => randomForFeatures.Next())
                        .Take(2)
                        .ToList();

                    foreach (var extra in extraFeatures)
                    {
                        hall.HallFeatures.Add(new HallFeature
                        {
                            HallId = hall.HallId,
                            FeatureId = extra.Id
                        });
                    }
                }
            }
            await context.SaveChangesAsync();

            // Ролі
            string[] roleNames = { Roles.Admin, Roles.User };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Створення Адміна
            var adminEmail = "admin@test.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Admin",
                    LastName = "System",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(adminUser, "AdminPass123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, Roles.Admin);
                }
            }

            // Користувачі
            var users = new List<ApplicationUser>();
            for (int i = 1; i <= 10; i++)
            {
                var email = $"user{i}@cinema.com";
                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    FirstName = $"User{i}",
                    LastName = "Test",
                    EmailConfirmed = true,
                    DateOfBirth = new DateTime(1990 + i, 1, 1)
                };

                var result = await userManager.CreateAsync(user, "Pa$$w0rd123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Roles.User);
                    users.Add(user);
                }
            }

            // Бронювання
            var random = new Random();
            var allSeats = await context.Seats.ToListAsync();
            var allSnacks = await context.Snacks.ToListAsync();
            var ticketList = new List<Ticket>();
            var bookingList = new List<Booking>();

            foreach (var session in sessions)
            {
                var sessionSeats = allSeats.Where(s => s.HallId == session.HallId).ToList();

                int percentage = random.Next(40, 61);
                int seatsToBookCount = (int)(sessionSeats.Count * (percentage / 100.0));
                var seatsToBook = sessionSeats.OrderBy(x => random.Next()).Take(seatsToBookCount).ToList();

                foreach (var seat in seatsToBook)
                {
                    var user = users[random.Next(users.Count)];

                    var booking = new Booking
                    {
                        ApplicationUserId = user.Id,
                        EmailAddress = user.Email,
                        CreatedDateTime = DateTime.Now.AddDays(-random.Next(1, 5)),
                        Tickets = new List<Ticket>()
                    };

                    var ticket = new Ticket
                    {
                        SessionId = session.SessionId,
                        SeatId = seat.SeatId,
                        Price = session.BasePrice * (decimal)seat.Coefficient,
                        Booking = booking
                    };
                    booking.Tickets.Add(ticket);

                    var statusRoll = random.Next(100);
                    var paymentStatus = statusRoll switch
                    {
                        < 80 => PaymentStatus.Completed,
                        < 90 => PaymentStatus.Refunded,
                        _ => PaymentStatus.Failed
                    };

                    var payment = new Payment
                    {
                        Amount = ticket.Price,
                        PaymentDate = booking.CreatedDateTime.AddMinutes(5),
                        Status = paymentStatus,
                        Booking = booking
                    };
                    booking.Payment = payment;

                    if (random.Next(2) == 0)
                    {
                        var snack = allSnacks[random.Next(allSnacks.Count)];
                        booking.SnackBookings.Add(new SnackBooking
                        {
                            SnackId = snack.SnackId,
                            Quantity = (byte)random.Next(1, 3)
                        });
                        payment.Amount += (snack.Price * booking.SnackBookings.First().Quantity);
                    }

                    bookingList.Add(booking);
                }

                if (bookingList.Count > 500)
                {
                    await context.Bookings.AddRangeAsync(bookingList);
                    await context.SaveChangesAsync();
                    bookingList.Clear();
                    ticketList.Clear();
                }
            }

            if (bookingList.Any())
            {
                await context.Bookings.AddRangeAsync(bookingList);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SaveChangesWithIdentityInsert(ApplicationDbContext context, string tableName)
        {
            await context.Database.OpenConnectionAsync();
            try
            {
                await context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT {tableName} ON");
                await context.SaveChangesAsync();
                await context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT {tableName} OFF");
            }
            finally
            {
                await context.Database.CloseConnectionAsync();
            }
        }
    }
}