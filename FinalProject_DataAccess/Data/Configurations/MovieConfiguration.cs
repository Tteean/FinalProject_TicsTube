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
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.Property(m => m.Title).IsRequired(true).HasMaxLength(30);
            builder.Property(m => m.Description).IsRequired(true).HasMaxLength(100);
            builder.Property(m => m.Director).IsRequired(true);
            builder.Property(m => m.Rating).IsRequired(true).HasColumnType("decimal(18,2)");
            builder.HasKey(m => m.Id);
            builder.HasMany(m => m.MovieGenres).WithOne(mg => mg.Movie).HasForeignKey(mg => mg.MovieId);
            builder.HasMany(m => m.MovieActors).WithOne(mg => mg.Movie).HasForeignKey(mg => mg.MovieId);



        }
    }
}
