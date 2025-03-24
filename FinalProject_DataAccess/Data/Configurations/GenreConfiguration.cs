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
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.Property(g=>g.Name).IsRequired(true).HasMaxLength(30);
            builder.HasKey(g => g.Id);
            builder.HasMany(g => g.MovieGenres).WithOne(mg => mg.Genre).HasForeignKey(mg => mg.GenreId);
        }
    }
}
