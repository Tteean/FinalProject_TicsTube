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
    public class ActorConfiguration : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.Property(g => g.Fullname).IsRequired(true).HasMaxLength(50);
            builder.HasKey(g => g.Id);
            builder.HasMany(g => g.MovieActors).WithOne(mg => mg.Actor).HasForeignKey(mg => mg.ActorId);
        }
    }
}
