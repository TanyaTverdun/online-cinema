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
    public class RequrimentConfiguration : IEntityTypeConfiguration<Requriment>
    {
        public void Configure(EntityTypeBuilder<Requriment> builder)
        {
            builder.HasKey(f => f.RequirementId);

            builder.Property(f => f.RequirementName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(f => f.RequirementDescription)
                .HasMaxLength(500);
        }
    }
}
