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
    public class FinancialTransactionConfiguration : IEntityTypeConfiguration<FinancialTransaction>
    {
        public void Configure(EntityTypeBuilder<FinancialTransaction> builder)
        {
            builder.HasKey(p => p.PaymentId);

            builder.Property(p => p.Amount)
                .HasColumnType("money");

            builder.Property(p => p.PaymentDate)
                .HasColumnType("datetime");

            builder.Property(p => p.Status)
                .HasColumnType("tinyint");

            builder.HasOne(p => p.CostumeBooking)
                .WithOne(b => b.FinancialTransaction)
                .HasForeignKey<CostumeBooking>(b => b.PaymentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
