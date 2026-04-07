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
    public class HallEquipmentConfiguration : IEntityTypeConfiguration<HallEquipment>
    {
        public void Configure(EntityTypeBuilder<HallEquipment> builder)
        {
            builder.HasKey(hf => new { hf.HallId, hf.RequirementId });

            builder.HasOne(hf => hf.DanceHall)
                .WithMany(h => h.HallEquipmentS)
                .HasForeignKey(hf => hf.HallId);

            builder.HasOne(hf => hf.Requriment)
                .WithMany(f => f.HallEquipment)
                .HasForeignKey(hf => hf.RequirementId);
        }
    }
}
