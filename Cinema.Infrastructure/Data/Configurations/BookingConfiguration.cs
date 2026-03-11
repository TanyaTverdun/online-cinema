using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Infrastructure.Data.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(b => b.BookingId);

            builder.Property(b => b.EmailAddress)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(b => b.CreatedDateTime)
                .HasColumnType("datetime");

            builder.HasOne(b => b.Payment)
               .WithOne(p => p.Booking)
               .HasForeignKey<Booking>(b => b.PaymentId)
               .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(b => b.ApplicationUser)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.ApplicationUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
