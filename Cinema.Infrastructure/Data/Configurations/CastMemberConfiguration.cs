using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Infrastructure.Data.Configurations
{
    public class CastMemberConfiguration : IEntityTypeConfiguration<CastMember>
    {
        public void Configure(EntityTypeBuilder<CastMember> builder)
        {
            builder.HasKey(c => c.CastId);

            builder.Property(c => c.CastFirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.CastLastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.CastMiddleName)
                .HasMaxLength(100);
        }
    }
}

