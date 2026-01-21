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
    public class MovieFeatureConfiguration : IEntityTypeConfiguration<MovieFeature>
    {
        public void Configure(EntityTypeBuilder<MovieFeature> builder)
        {
            builder.HasKey(mf => new { mf.MovieId, mf.FeatureId });

            builder.HasOne(mf => mf.Movie)
                .WithMany(m => m.MovieFeatures)
                .HasForeignKey(mf => mf.MovieId);

            builder.HasOne(mf => mf.Feature)
                .WithMany(f => f.MovieFeatures)
                .HasForeignKey(mf => mf.FeatureId);
        }
    }
}
