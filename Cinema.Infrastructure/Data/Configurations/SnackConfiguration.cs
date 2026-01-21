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
    public class SnackConfiguration : IEntityTypeConfiguration<Snack>
    {
        public void Configure(EntityTypeBuilder<Snack> builder)
        {
            builder.HasKey(s => s.SnackId);

            builder.Property(s => s.SnackName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(s => s.Price)
                .HasColumnType("money");

            builder.HasMany(s => s.SnackBookings)
                .WithOne(sb => sb.Snack)
                .HasForeignKey(sb => sb.SnackId);
        }
    }
}
