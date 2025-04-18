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
    public class TVShowGenreConfiguration : IEntityTypeConfiguration<TVShowGenre>
    {
        public void Configure(EntityTypeBuilder<TVShowGenre> builder)
        {
            builder.HasKey(mg => new { mg.TVShowId, mg.GenreId });
            builder.HasOne(mg => mg.TVShow).WithMany(m => m.TVShowGenres).HasForeignKey(mg => mg.TVShowId);
            builder.HasOne(mg => mg.Genre).WithMany(g => g.TVShowGenres).HasForeignKey(mg => mg.GenreId);
        }
    }
}
