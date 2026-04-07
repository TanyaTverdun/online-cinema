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
    public class PerformanceRequirementConfiguration : IEntityTypeConfiguration<PerformanceRequirement>
    {
        public void Configure(EntityTypeBuilder<PerformanceRequirement> builder)
        {
            builder.HasKey(mf => new { mf.RequirementId, mf.PerformanceId});

            builder.HasOne(mf => mf.Performance)
                .WithMany(m => m.PerformanceRequirements)
                .HasForeignKey(mf => mf.PerformanceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(mf => mf.Requriment)
                .WithMany(f => f.PerformanceRequirements)
                .HasForeignKey(mf => mf.RequirementId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
