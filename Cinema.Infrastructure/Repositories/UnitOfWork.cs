using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using onlineCinema.Application.Interfaces;
using onlineCinema.Domain.Entities;
using onlineCinema.Infrastructure.Data;

namespace onlineCinema.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IBookingRepository Booking { get; private set; }
        public ICastMemberRepository CastMember { get; private set; }
        public ICinemaRepository Cinema { get; private set; }
        public IDirectorRepository Director { get; private set; }
        public IMovieRepository Movie { get; private set; }
        public IFeatureRepository Feature { get; private set; }
        public IGenreRepository Genre { get; private set; }
        public IHallRepository Hall { get; private set; }
        public ILanguageRepository Language { get; private set; }
        public IPaymentRepository Payment { get; private set; }
        public ISeatRepository Seat { get; private set; }
        public ISessionRepository Session { get; private set; }
        public ISnackRepository Snack { get; private set; }
        public ITicketRepository Ticket { get; private set; }
        public IStatisticsRepository Statistics { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            this.Booking = new BookingRepository(_db);
            this.CastMember = new CastMemberRepository(_db);
            this.Cinema = new CinemaRepository(_db);
            this.Director = new DirectorRepository(_db);
            this.Movie = new MovieRepository(_db);
            this.Feature = new FeatureRepository(_db);
            this.Genre = new GenreRepository(_db);
            this.Hall = new HallRepository(_db);
            this.Language = new LanguageRepository(_db);
            this.Payment = new PaymentRepository(_db);
            this.Seat = new SeatRepository(_db);
            this.Session = new SessionRepository(_db);
            this.Snack = new SnackRepository(_db);
            this.Ticket = new TicketRepository(_db);
            this.Statistics = new StatisticsRepository(_db);

        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
