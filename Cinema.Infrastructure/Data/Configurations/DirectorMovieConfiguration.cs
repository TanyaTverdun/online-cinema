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
    public class DirectorMovieConfiguration : IEntityTypeConfiguration<DirectorMovie>
    {
        public void Configure(EntityTypeBuilder<DirectorMovie> builder)
        {
            builder.HasKey(dm => new { dm.DirectorId, dm.MovieId });

            builder.HasOne(dm => dm.Director)
                .WithMany(d => d.DirectorMovies)
                .HasForeignKey(dm => dm.DirectorId);

            builder.HasOne(dm => dm.Movie)
                .WithMany(m => m.MovieDirectors)
                .HasForeignKey(dm => dm.MovieId);
        }
    }
}
