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
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(m => m.Status)
                .HasColumnType("tinyint");

            builder.Property(m => m.AgeRating)
                .HasColumnType("tinyint");

            builder.Property(m => m.ReleaseDate)
                .HasColumnType("date");

            builder.Property(m => m.TrailerLink).HasMaxLength(2048);
            builder.Property(m => m.PosterImage).HasMaxLength(2048);
        }
    }
}
