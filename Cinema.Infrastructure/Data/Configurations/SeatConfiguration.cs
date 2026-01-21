using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Infrastructure.Data.Configurations
{
    public class SeatConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            builder.HasKey(s => s.SeatId);

            builder.Property(s => s.HallId)
                   .IsRequired();

            builder.Property(s => s.RowNumber)
                   .HasColumnType("tinyint")
                   .IsRequired();

            builder.Property(s => s.SeatNumber)
                   .HasColumnType("tinyint")
                   .IsRequired();

            builder.Property(s => s.Type)
                   .HasColumnType("tinyint")
                   .IsRequired();

            builder.Property(s => s.Coefficient)
                   .HasColumnType("float")
                   .IsRequired();

            builder.HasOne(s => s.Hall)
                   .WithMany(h => h.Seats)
                   .HasForeignKey(s => s.HallId);

            builder.HasIndex(s => new { s.HallId, s.RowNumber, s.SeatNumber })
                   .IsUnique();
        }
    }
}
