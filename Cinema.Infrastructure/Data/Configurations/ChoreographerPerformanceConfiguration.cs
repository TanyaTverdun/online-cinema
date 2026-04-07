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
    public class ChoreographerPerformanceConfiguration : IEntityTypeConfiguration<ChoreographerPerformance>
    {
        public void Configure(EntityTypeBuilder<ChoreographerPerformance> builder)
        {
            builder.HasKey(dm => new { dm.ChoreographerId, dm.PerformanceId });

            builder.HasOne(dm => dm.Choreographer)
                .WithMany(d => d.ChoreographerPerformances)
                .HasForeignKey(dm => dm.ChoreographerId);

            builder.HasOne(dm => dm.Performance)
                .WithMany(m => m.ChoreographerPerformances)
                .HasForeignKey(dm => dm.PerformanceId);
        }
    }
}
