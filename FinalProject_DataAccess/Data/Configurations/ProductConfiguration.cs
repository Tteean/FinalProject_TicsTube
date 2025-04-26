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
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(m => m.Name).IsRequired(true).HasMaxLength(30);
            builder.Property(m => m.Description).IsRequired(true).HasMaxLength(500);
            builder.Property(m => m.Image).IsRequired(true);
            builder.Property(m => m.InStock).IsRequired(true);
            builder.Property(m => m.CostPrice).IsRequired(true).HasColumnType("decimal(18,2)");
            builder.HasKey(m => m.Id);
            builder.HasMany(m => m.MovieProducts).WithOne(mg => mg.Product).HasForeignKey(mg => mg.ProductId);
            builder.HasMany(m => m.tvShowProducts).WithOne(ma => ma.Product).HasForeignKey(ma => ma.ProductId);
        }
    }
}
