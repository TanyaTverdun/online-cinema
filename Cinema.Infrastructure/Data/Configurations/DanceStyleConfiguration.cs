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
    public class DanceStyleConfiguration : IEntityTypeConfiguration<DanceStyle>
    {
        public void Configure(EntityTypeBuilder<DanceStyle> builder)
        {
            
            builder.HasKey(g => g.StyleId);

          
            builder.Property(g => g.StyleName)
                .IsRequired()       
                .HasMaxLength(50);  
        }
    }
}