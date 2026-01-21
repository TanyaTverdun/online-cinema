using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Infrastructure.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.LastName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.MiddleName)
                .HasMaxLength(100);

            builder.Property(u => u.DateOfBirth)
                .HasColumnType("date");

            builder.HasMany(u => u.Bookings)
                .WithOne(b => b.ApplicationUser)
                .HasForeignKey(b => b.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
