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
    public class MovieProductConfiguration : IEntityTypeConfiguration<MovieProduct>
    {
        public void Configure(EntityTypeBuilder<MovieProduct> builder)
        {
            builder.HasKey(mg => new { mg.MovieId, mg.ProductId });
            builder.HasOne(mg => mg.Movie).WithMany(m => m.MovieProducts).HasForeignKey(mg => mg.MovieId);
            builder.HasOne(mg => mg.Product).WithMany(g => g.MovieProducts).HasForeignKey(mg => mg.ProductId);
        }
    }
}
