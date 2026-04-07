using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Infrastructure.Data.Configurations
{
    public class InventaryConfiguration : IEntityTypeConfiguration<Inventary>
    {
        public void Configure(EntityTypeBuilder<Inventary> builder)
        {
            builder.HasKey(s => s.ItemId);

            builder.Property(s => s.HallId)
                   .IsRequired();

            builder.Property(s => s.Category)
                   .HasMaxLength(100)
                   .IsRequired();
            builder.Property(s => s.IdentifiertNumber) 
                   .IsRequired();
            builder.Property(s => s.Type)
                   .HasColumnType("tinyint")
                   .IsRequired();

            builder.HasOne(s => s.DanceHall)
                .WithMany(h => h.Inventaries)
                .HasForeignKey(s => s.HallId)
                .OnDelete(DeleteBehavior.Cascade);

          
            builder.HasMany(s => s.AttendanceLogs)
                .WithOne(a => a.Inventary)
                .HasForeignKey(a => a.ItemId);

            
            builder.HasIndex(s => new { s.HallId, s.IdentifiertNumber })
                .IsUnique();
        }
    }
}
