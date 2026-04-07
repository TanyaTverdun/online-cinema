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
    public class MerchOrderConfiguration : IEntityTypeConfiguration<MerchOrder>
    {
        public void Configure(EntityTypeBuilder<MerchOrder> builder)
        {
            builder.HasKey(sb => new { sb.ProductId, sb.BookingId });

            builder.HasOne(sb => sb.StudioMerch)
                .WithMany(s => s.MerchOrders)
                .HasForeignKey(sb => sb.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sb => sb.CostumeBooking)
                .WithMany(b => b.MerchOrders)
                .HasForeignKey(sb => sb.BookingId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
