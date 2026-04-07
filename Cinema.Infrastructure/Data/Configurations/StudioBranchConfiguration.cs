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
    public class StudioBranchConfiguration : IEntityTypeConfiguration<StudioBranch>
    {
        public void Configure(EntityTypeBuilder<StudioBranch> builder)
        {
            builder.HasKey(c => c.BranchId);
            builder.Property(c => c.BranchId);

            builder.Property(c => c.BranchName)
                .IsRequired()
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

            builder.HasMany(c => c.DanceHalls)
                .WithOne(h => h.StudioBranch)
                .HasForeignKey(h => h.BranchId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
