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
    public class PerformanceConfiguration : IEntityTypeConfiguration<Performance>
    {
        public void Configure(EntityTypeBuilder<Performance> builder)
        {
            builder.HasKey(m => m.PerformanceId);//

            builder.Property(m => m.Title)//
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(m => m.Status)//
                .HasColumnType("tinyint");

            builder.Property(m => m.AgeCategory)//
                .HasColumnType("tinyint");

            builder.Property(m => m.StartDate)
                .HasColumnType("date");

            builder.Property(m => m.VideoLink).HasMaxLength(2048);
            builder.Property(m => m.CoverImage).HasMaxLength(2048);
        }
    }
}
