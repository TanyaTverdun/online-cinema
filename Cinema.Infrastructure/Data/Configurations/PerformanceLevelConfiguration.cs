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
    public class PerformanceLevelConfiguration : IEntityTypeConfiguration<PerformanceLevel>
    {
        public void Configure(EntityTypeBuilder<PerformanceLevel> builder)
        {
            builder.HasKey(lm => new { lm.LevelId, lm.PerformanceId });

            builder.HasOne(lm => lm.SkillLevel)
                .WithMany(l => l.PerformanceLevels)
                .HasForeignKey(lm => lm.LevelId);

            builder.HasOne(lm => lm.Performance)
                .WithMany(m => m.PerformanceLevels)
                .HasForeignKey(lm => lm.PerformanceId);
        }
    }
}
