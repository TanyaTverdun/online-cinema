using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using onlineCinema.Application.Interfaces;
using onlineCinema.Domain.Entities;
using onlineCinema.Infrastructure.Data;

namespace onlineCinema.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public ICostumeBookingRepository Booking { get; private set; }
        public IDancerRepository CastMember { get; private set; }
        public IStudioBranchRepository Cinema { get; private set; }
        public IChoreographerRepository Director { get; private set; }
        public IPerformanceRepository Movie { get; private set; }
        public IRequrimentRepository Feature { get; private set; }
        public IDanceStyleRepository Genre { get; private set; }
        public IDanceHallRepository Hall { get; private set; }
        public ISkillLevelRepository Language { get; private set; }

        // Виправив типи для фінансів та статистики
        public IFinancialTransactionRepository Payment { get; private set; }
        public IInventaryRepository Seat { get; private set; }
        public IDanceClassRepository Session { get; private set; }
        public IStudioMerchRepository Snack { get; private set; }
        public IAttendanceLogRepository Ticket { get; private set; }
        public IStatisticRepository Statistics { get; private set; } // Тепер IStatisticRepository

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            this.Booking = new CostumeBookingRepository(_db);
            this.CastMember = new DancerRepository(_db);
            this.Cinema = new StudioBranchRepository(_db);
            this.Director = new ChoreographerRepository(_db);
            this.Movie = new PerformanceRepository(_db);
            this.Feature = new RequrimentRepository(_db);
            this.Genre = new DanceStyleRepository(_db);
            this.Hall = new DanceHallRepository(_db);
            this.Language = new SkillLevelRepository(_db);

            // Важливо: Payment і Statistics тепер мають різні репозиторії
            this.Payment = new FinancialTransactionRepository(_db);
            this.Seat = new InventaryRepository(_db);
            this.Session = new DanceClassRepository(_db);
            this.Snack = new StudioMerchRepository(_db);
            this.Ticket = new AttendanceLogRepository(_db);
            this.Statistics = new StatisticRepository(_db); // Виправив на StatisticRepository
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}