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
    public class CinemaConfiguration : IEntityTypeConfiguration<Cinema>
    {
        public void Configure(EntityTypeBuilder<Cinema> builder)
        {
            builder.HasKey(c => c.CinemaId);
            builder.Property(c => c.CinemaId);

            builder.Property(c => c.CinemaName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Region)
                .HasMaxLength(100);

            builder.Property(c => c.City)
                .HasMaxLength(100);

            builder.Property(c => c.Street)
                .HasMaxLength(100);

            builder.Property(c => c.Building);

            builder.Property(c => c.TimeOpen)
                .HasColumnType("time");

            builder.Property(c => c.TimeClose)
                .HasColumnType("time");

            builder.HasMany(c => c.Halls)
                .WithOne(h => h.Cinema)
                .HasForeignKey(h => h.CinemaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
