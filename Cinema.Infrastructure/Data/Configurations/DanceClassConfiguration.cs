using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Infrastructure.Data.Configurations
{
    public class DanceClassConfiguration : IEntityTypeConfiguration<DanceClass>
    {
        public void Configure(EntityTypeBuilder<DanceClass> builder)
        {
            builder.HasKey(s => s.ClassId);

            builder.Property(s => s.PerformanceId)
                   .IsRequired();

            builder.Property(s => s.HallId)
                   .IsRequired();

            builder.Property(s => s.StartDateTime)
                   .HasColumnType("datetime")
                   .IsRequired();

            builder.Property(s => s.DropinPrice)
                   .HasColumnType("money")
                   .IsRequired();

            builder.HasOne(s => s.Performance)
                   .WithMany(m => m.DanceClassesS)
                   .HasForeignKey(s => s.PerformanceId);

            builder.HasOne(s => s.DancerHall)
                   .WithMany(h => h.DanceClasses)
                   .HasForeignKey(s => s.HallId);

            builder.HasIndex(s => new { s.HallId, s.StartDateTime})
                   .IsUnique();
        }
    }
}
