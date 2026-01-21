using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Infrastructure.Data.Configurations
{
    public class HallConfiguration : IEntityTypeConfiguration<Hall>
    {
        public void Configure(EntityTypeBuilder<Hall> builder)
        {
            builder.HasKey(h => h.HallId);

            builder.Property(h => h.CinemaId)
                   .IsRequired();

            builder.Property(h => h.HallNumber)
                   .HasColumnType("tinyint")
                   .IsRequired();

            builder.Property(h => h.RowCount)
                   .HasColumnType("tinyint")
                   .IsRequired();

            builder.Property(h => h.SeatInRowCount)
                   .HasColumnType("tinyint")
                   .IsRequired();

            builder.HasOne(h => h.Cinema)
                   .WithMany(c => c.Halls)
                   .HasForeignKey(h => h.CinemaId);
        }
    }
}
