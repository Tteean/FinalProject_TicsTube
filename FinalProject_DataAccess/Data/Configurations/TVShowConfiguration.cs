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
    public class TVShowConfiguration : IEntityTypeConfiguration<TVShow>
    {
        public void Configure(EntityTypeBuilder<TVShow> builder)
        {
            builder.Property(t => t.Title).IsRequired(true).HasMaxLength(30);
            builder.Property(t => t.Description).IsRequired(true).HasMaxLength(500);
            builder.Property(t => t.Image).IsRequired(true);
            builder.HasKey(t => t.Id);
            builder.HasMany(t => t.TVShowGenres).WithOne(tg => tg.TVShow).HasForeignKey(tg => tg.TVShowId);
            builder.HasMany(t => t.TVShowActors).WithOne(ta => ta.TVShow).HasForeignKey(ta => ta.TVShowId);
            builder.HasMany(t => t.TVShowLanguages).WithOne(tl => tl.TVShow).HasForeignKey(tl => tl.TVShowId);
            builder.HasOne(t => t.Directors).WithMany(d => d.TVShows).HasForeignKey(s => s.DirectorId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
