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
    public class SeasonConfiguration : IEntityTypeConfiguration<Season>
    {
        public void Configure(EntityTypeBuilder<Season> builder)
        {
            builder.HasOne(s => s.TVShow).WithMany(t => t.Seasons).HasForeignKey(s => s.TVShowId);
            builder.Property(s => s.SeasonNumber).IsRequired(true);
        }
    }
}
