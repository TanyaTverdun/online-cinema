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
    public class HallFeatureConfiguration : IEntityTypeConfiguration<HallFeature>
    {
        public void Configure(EntityTypeBuilder<HallFeature> builder)
        {
            builder.HasKey(hf => new { hf.HallId, hf.FeatureId });

            builder.HasOne(hf => hf.Hall)
                .WithMany(h => h.HallFeatures)
                .HasForeignKey(hf => hf.HallId);

            builder.HasOne(hf => hf.Feature)
                .WithMany(f => f.HallFeatures)
                .HasForeignKey(hf => hf.FeatureId);
        }
    }
}
