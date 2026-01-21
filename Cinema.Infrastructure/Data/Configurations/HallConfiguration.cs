using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Infrastructure.Data.Configurations
{
    public class HallConfiguration: IEntityTypeConfiguration<Hall>
    {
        public void Configure(EntityTypeBuilder<Hall> builder)
        {
            builder.HasKey(h => h.HallId);

            builder.Property(h => h.HallId)
                   .HasColumnName("hall_id");

            builder.Property(h => h.CinemaId)
                   .HasColumnName("cinema_id")
                   .IsRequired();

            builder.Property(h => h.HallNumber)
                   .HasColumnName("hall_num")
                   .HasColumnType("tinyint")
                   .IsRequired();

            builder.Property(h => h.RowCount)
                   .HasColumnName("row_count")
                   .HasColumnType("tinyint")
                   .IsRequired();

            builder.Property(h => h.SeatInRowCount)
                   .HasColumnName("seat_in_row_count")
                   .HasColumnType("tinyint")
                   .IsRequired();

            builder.HasOne(h => h.Cinema)
                   .WithMany(c => c.Halls)
                   .HasForeignKey(h => h.CinemaId);
        }
    }
}
