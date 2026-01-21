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
    public class DirectorConfiguration : IEntityTypeConfiguration<Director>
    {
        public void Configure(EntityTypeBuilder<Director> builder)
        {
         
            builder.HasKey(d => d.DirectorId);

           
            builder.Property(d => d.DirectorFirstName)
                .IsRequired()       
                .HasMaxLength(100); 

            builder.Property(d => d.DirectorLastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.DirectorMiddleName)
                .HasMaxLength(100); 
        }
    }
}

