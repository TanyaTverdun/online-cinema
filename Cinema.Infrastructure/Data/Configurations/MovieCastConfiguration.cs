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
    public class MovieCastConfiguration : IEntityTypeConfiguration<MovieCast>
    {
        public void Configure(EntityTypeBuilder<MovieCast> builder)
        {
            builder.HasKey(mc => new { mc.MovieId, mc.CastId });

            builder.HasOne(mc => mc.Movie)
                .WithMany(m => m.MovieCasts)
                .HasForeignKey(mc => mc.MovieId);

            builder.HasOne(mc => mc.CastMember)
                .WithMany(c => c.MovieCasts)
                .HasForeignKey(mc => mc.CastId);
        }
    }
}
