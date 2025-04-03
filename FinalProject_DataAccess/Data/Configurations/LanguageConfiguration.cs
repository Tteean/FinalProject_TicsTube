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
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.Property(g => g.Name).IsRequired(true).HasMaxLength(50);
            builder.HasKey(g => g.Id);
            builder.HasMany(g => g.MovieLanguages).WithOne(mg => mg.Language).HasForeignKey(mg => mg.LanguageId);
        }
    }
}
