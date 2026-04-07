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
    public class ChoreographerConfiguration : IEntityTypeConfiguration<Choreographer>
    {
        public void Configure(EntityTypeBuilder<Choreographer> builder)
        {
         
            builder.HasKey(d => d.ChoreographerId);

           
            builder.Property(d => d.FirstName)
                .IsRequired()       
                .HasMaxLength(100); 

            builder.Property(d => d.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.MiddleName)
                .HasMaxLength(100); 
        }
    }
}

