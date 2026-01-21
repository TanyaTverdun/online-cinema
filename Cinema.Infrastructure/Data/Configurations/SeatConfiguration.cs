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

            builder.Property(s => s.SeatId)
                   .HasColumnName("seat_id");

            builder.Property(s => s.HallId)
                   .HasColumnName("hall_id")
                   .IsRequired();

            builder.Property(s => s.RowNumber)
                   .HasColumnName("row_number")
                   .HasColumnType("tinyint")
                   .IsRequired();

            builder.Property(s => s.SeatNumber)
                   .HasColumnName("seat_number")
                   .HasColumnType("tinyint")
                   .IsRequired();

            builder.Property(s => s.Type)
                   .HasColumnName("type")
                   .HasColumnType("tinyint")
                   .IsRequired();

            builder.Property(s => s.Coefficient)
                   .HasColumnName("coefficient")
                   .HasColumnType("float")
                   .IsRequired();

            builder.HasOne(s => s.Hall)
                   .WithMany(h => h.Seats)
                   .HasForeignKey(s => s.HallId);

            // Рекомендовано для логічної цілісності
            builder.HasIndex(s => new { s.HallId, s.RowNumber, s.SeatNumber })
                   .IsUnique();
        }
    }
}
