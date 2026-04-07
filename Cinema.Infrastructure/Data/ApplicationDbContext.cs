using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<DanceMember>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Основні таблиці студії
        public DbSet<StudioBranch> StudioBranches { get; set; } = null!;
        public DbSet<DanceHall> DanceHalls { get; set; } = null!;
        public DbSet<Inventory> Inventories { get; set; } = null!; // Пофікшено: Inventory замість Inventary
        public DbSet<Requirement> Requirements { get; set; } = null!; // Пофікшено: Requirement замість Requriment

        // Розклад та постановки
        public DbSet<Performance> Performances { get; set; } = null!;
        public DbSet<DanceClass> DanceClasses { get; set; } = null!;
        public DbSet<DanceStyle> DanceStyles { get; set; } = null!;
        public DbSet<SkillLevel> SkillLevels { get; set; } = null!;

        // Люди
        public DbSet<Choreographer> Choreographers { get; set; } = null!;
        public DbSet<Dancer> Dancers { get; set; } = null!;

        // Процеси та фінанси
        public DbSet<AttendanceLog> AttendanceLogs { get; set; } = null!;
        public DbSet<CostumeBooking> CostumeBookings { get; set; } = null!;
        public DbSet<FinancialTransaction> FinancialTransactions { get; set; } = null!;
        public DbSet<StudioMerch> StudioMerches { get; set; } = null!;

        // Проміжні таблиці (Join Tables) - за бажанням можна додати і їх для зручних запитів
        public DbSet<PerformanceStyle> PerformanceStyles { get; set; } = null!;
        public DbSet<PerformanceDancers> PerformanceDancers { get; set; } = null!;
        public DbSet<ChoreographerPerformance> ChoreographerPerformances { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Важливо: спочатку викликаємо base, щоб Identity налаштував свої таблиці
            base.OnModelCreating(builder);

            // Автоматично застосовуємо всі конфігурації з папки Configurations
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}