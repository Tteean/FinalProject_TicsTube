using FinalProject_Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_DataAccess.Data.Configurations
{
    public class TVShowProductConfiguration : IEntityTypeConfiguration<TvShowProduct>
    {
        public void Configure(EntityTypeBuilder<TvShowProduct> builder)
        {
            builder.HasKey(mg => new { mg.TvShowId, mg.ProductId });
            builder.HasOne(mg => mg.TVShow).WithMany(m => m.tvShowProducts).HasForeignKey(mg => mg.TvShowId);
            builder.HasOne(mg => mg.Product).WithMany(g => g.tvShowProducts).HasForeignKey(mg => mg.ProductId);
        }
    }
}
