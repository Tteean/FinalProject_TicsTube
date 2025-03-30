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
    public class MovieActorConfiguration : IEntityTypeConfiguration<MovieActor>
    {
        public void Configure(EntityTypeBuilder<MovieActor> builder)
        {
            builder.HasKey(mg => new { mg.MovieId, mg.ActorId });
            builder.HasOne(mg => mg.Movie).WithMany(m => m.MovieActors).HasForeignKey(mg => mg.MovieId);
            builder.HasOne(mg => mg.Actor).WithMany(g => g.MovieActors).HasForeignKey(mg => mg.ActorId);
        }
    }
}
