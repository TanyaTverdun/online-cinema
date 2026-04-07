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
    public class DancerConfiguration : IEntityTypeConfiguration<Dancer>
    {
        public void Configure(EntityTypeBuilder<Dancer> builder)
        {
            builder.HasKey(c => c.DancerId);

            builder.Property(c => c.DancerFirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.DancerLastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.DancerMiddleName)
                .HasMaxLength(100);
        }
    }
}

