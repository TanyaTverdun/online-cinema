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
    public class PerformanceStyleConfiguration : IEntityTypeConfiguration<PerformanceStyle>
    {
        public void Configure(EntityTypeBuilder<PerformanceStyle> builder)
        {
            builder.HasKey(mg => new { mg.PerformanceId, mg.StyleId });

            builder.HasOne(mg => mg.Performance)
                .WithMany(m => m.PerformanceStyles)
                .HasForeignKey(mg => mg.PerformanceId);

            builder.HasOne(mg => mg.DanceStyle)
                .WithMany(g => g.PerformanceStyles)
                .HasForeignKey(mg => mg.StyleId);
        }
    }
}
