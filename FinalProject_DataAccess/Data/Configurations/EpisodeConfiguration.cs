using FinalProject_Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_DataAccess.Data.Configurations
{
    public class EpisodeConfiguration : IEntityTypeConfiguration<Episode>
    {
        public void Configure(EntityTypeBuilder<Episode> builder)
        {
            builder.HasOne(e => e.Seasons).WithMany(s => s.Episodes).HasForeignKey(e => e.SeasonId);
            builder.Property(m => m.Title).IsRequired(true).HasMaxLength(30);
            builder.Property(m => m.Description).IsRequired(true).HasMaxLength(500);
            builder.Property(m => m.Duration).IsRequired(true);
            builder.Property(m => m.Video).IsRequired(true);
            builder.Property(m => m.Image).IsRequired(true);
        }
    }
}
