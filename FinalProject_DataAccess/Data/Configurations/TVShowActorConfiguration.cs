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
    public class TVShowActorConfiguration : IEntityTypeConfiguration<TVShowActor>
    {
        public void Configure(EntityTypeBuilder<TVShowActor> builder)
        {
            builder.HasKey(mg => new { mg.TVShowId, mg.ActorId });
            builder.HasOne(mg => mg.TVShow).WithMany(m => m.TVShowActors).HasForeignKey(mg => mg.TVShowId);
            builder.HasOne(mg => mg.Actor).WithMany(g => g.TVShowActors).HasForeignKey(mg => mg.ActorId).IsRequired(false);
        }
    }
}
