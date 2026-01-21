using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineCinema.Domain.Entities;

namespace onlineCinema.Infrastructure.Data.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.PaymentId);

            builder.Property(p => p.Amount)
                .HasColumnType("money");

            builder.Property(p => p.PaymentDate)
                .HasColumnType("datetime");

            builder.Property(p => p.Status)
                .HasColumnType("tinyint");

            // One Payment -> many Bookings
            builder.HasMany(p => p.Bookings)
                .WithOne(b => b.Payment)
                .HasForeignKey(b => b.PaymentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
