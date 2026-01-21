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
    public class LanguageMovieConfiguration : IEntityTypeConfiguration<LanguageMovie>
    {
        public void Configure(EntityTypeBuilder<LanguageMovie> builder)
        {
            builder.HasKey(lm => new { lm.LanguageId, lm.MovieId });

            builder.HasOne(lm => lm.Language)
                .WithMany(l => l.LanguageMovies)
                .HasForeignKey(lm => lm.LanguageId);

            builder.HasOne(lm => lm.Movie)
                .WithMany(m => m.MovieLanguages)
                .HasForeignKey(lm => lm.MovieId);
        }
    }
}
