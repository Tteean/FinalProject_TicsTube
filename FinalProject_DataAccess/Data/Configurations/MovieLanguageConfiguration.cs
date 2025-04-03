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
    public class MovieLanguageConfiguration : IEntityTypeConfiguration<MovieLanguage>
    {
        public void Configure(EntityTypeBuilder<MovieLanguage> builder)
        {
            builder.HasKey(mg => new { mg.MovieId, mg.LanguageId });
            builder.HasOne(mg => mg.Movie).WithMany(m => m.MovieLanguages).HasForeignKey(mg => mg.MovieId);
            builder.HasOne(mg => mg.Language).WithMany(g => g.MovieLanguages).HasForeignKey(mg => mg.LanguageId);
        }
    }
}
