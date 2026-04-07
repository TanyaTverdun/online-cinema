using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Infrastructure.Data.Configurations
{
    public class DanceHallConfiguration : IEntityTypeConfiguration<DanceHall>
    {
        public void Configure(EntityTypeBuilder<DanceHall> builder)
        {
            builder.HasKey(h => h.HallId);

            builder.Property(h => h.BranchId)
                   .IsRequired();

            builder.Property(h => h.HallNumber)
                   .HasColumnType("tinyint")
                   .IsRequired();

            builder.Property(h => h.AreaSize)
                   .IsRequired();

            builder.Property(h => h.MaxPeople)
                   .IsRequired();

            builder.HasOne(h => h.StudioBranch)
                   .WithMany(c => c.DanceHalls)
                   .HasForeignKey(h => h.BranchId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
