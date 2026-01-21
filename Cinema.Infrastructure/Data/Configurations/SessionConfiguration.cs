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

            builder.Property(s => s.SessionId)
                   .HasColumnName("session_id");

            builder.Property(s => s.MovieId)
                   .HasColumnName("movie_id")
                   .IsRequired();

            builder.Property(s => s.HallId)
                   .HasColumnName("hall_id")
                   .IsRequired();

            builder.Property(s => s.ShowingDateTime)
                   .HasColumnName("showing_datetime")
                   .HasColumnType("datetime")
                   .IsRequired();

            builder.Property(s => s.BasePrice)
                   .HasColumnName("base_price")
                   .HasColumnType("money")
                   .IsRequired();

            builder.HasOne(s => s.Movie)
                   .WithMany(m => m.Sessions)
                   .HasForeignKey(s => s.MovieId);

            builder.HasOne(s => s.Hall)
                   .WithMany(h => h.Sessions)
                   .HasForeignKey(s => s.HallId);

            // Рекомендовано: одна сесія в одному залі на конкретний час
            builder.HasIndex(s => new { s.HallId, s.ShowingDateTime })
                   .IsUnique();
        }
    }
}
