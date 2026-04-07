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
    public class AttendanceLogConfiguration : IEntityTypeConfiguration<Domain.Entities.AttendanceLog>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.AttendanceLog> builder)
        {
            builder.HasKey(t => t.AttendanceId);
            builder.Property(t => t.ActualPrice)
                .HasColumnType("money");

            builder.HasOne(t => t.DanceClass)
                .WithMany(s => s.AttendanceLogs)
                .HasForeignKey(t => t.ClassId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Inventary)
                .WithMany(s => s.AttendanceLogs)
                .HasForeignKey(t => t.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.CostumeBooking)
                .WithMany(b => b.AttendanceLogs)
                .HasForeignKey(t => t.BookingId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
