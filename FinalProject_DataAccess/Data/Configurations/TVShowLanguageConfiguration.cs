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
    public class TVShowLanguageConfiguration : IEntityTypeConfiguration<TVShowLanguage>
    {
        public void Configure(EntityTypeBuilder<TVShowLanguage> builder)
        {
            builder.HasKey(mg => new { mg.TVShowId, mg.LanguageId });
            builder.HasOne(mg => mg.TVShow).WithMany(m => m.TVShowLanguages).HasForeignKey(mg => mg.TVShowId);
            builder.HasOne(mg => mg.Language).WithMany(g => g.TVShowLanguages).HasForeignKey(mg => mg.LanguageId);
        }
    }
}
