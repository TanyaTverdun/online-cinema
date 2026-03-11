using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Infrastructure.Data.Configurations
{
    public class SnackBookingConfiguration : IEntityTypeConfiguration<SnackBooking>
    {
        public void Configure(EntityTypeBuilder<SnackBooking> builder)
        {
            builder.HasKey(sb => new { sb.SnackId, sb.BookingId });

            builder.HasOne(sb => sb.Snack)
                .WithMany(s => s.SnackBookings)
                .HasForeignKey(sb => sb.SnackId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sb => sb.Booking)
                .WithMany(b => b.SnackBookings)
                .HasForeignKey(sb => sb.BookingId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
