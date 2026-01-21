using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using onlineCinema.Domain.Entities;

namespace onlineCinema.Infrastructure.Data.Configurations
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.HasKey(s => s.SessionId);

            builder.Property(s => s.MovieId)
                   .IsRequired();

            builder.Property(s => s.HallId)
                   .IsRequired();

            builder.Property(s => s.ShowingDateTime)
                   .HasColumnType("datetime")
                   .IsRequired();

            builder.Property(s => s.BasePrice)
                   .HasColumnType("money")
                   .IsRequired();

            builder.HasOne(s => s.Movie)
                   .WithMany(m => m.Sessions)
                   .HasForeignKey(s => s.MovieId);

            builder.HasOne(s => s.Hall)
                   .WithMany(h => h.Sessions)
                   .HasForeignKey(s => s.HallId);

            builder.HasIndex(s => new { s.HallId, s.ShowingDateTime })
                   .IsUnique();
        }
    }
}
