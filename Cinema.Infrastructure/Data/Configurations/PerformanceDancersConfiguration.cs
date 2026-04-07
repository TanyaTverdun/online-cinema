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
    public class PerformanceDancersConfiguration : IEntityTypeConfiguration<PerformanceDancers>
    {
        public void Configure(EntityTypeBuilder<PerformanceDancers> builder)
        {
            builder.HasKey(mc => new { mc.PerformanceId, mc.DancerId });

            builder.HasOne(mc => mc.Performance)
                .WithMany(m => m.PerformanceDancers)
                .HasForeignKey(mc => mc.PerformanceId);

            builder.HasOne(mc => mc.Dancer)
                .WithMany(c => c.PerformanceDancers)
                .HasForeignKey(mc => mc.DancerId);
        }
    }
}
