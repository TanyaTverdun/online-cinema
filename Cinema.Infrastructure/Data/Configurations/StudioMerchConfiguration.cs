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
    public class StudioMerchConfiguration : IEntityTypeConfiguration<StudioMerch>
    {
        public void Configure(EntityTypeBuilder<StudioMerch> builder)
        {
            builder.HasKey(s => s.ProductId);

            builder.Property(s => s.ProductName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(s => s.Price)
                .HasColumnType("money");

            builder.HasMany(s => s.MerchOrders)
                .WithOne(sb => sb.StudioMerch)
                .HasForeignKey(sb => sb.ProductId);
        }
    }
}
