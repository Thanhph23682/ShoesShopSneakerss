using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APPDATA.Models;

namespace APPDATA.Configurations
{
    public class ProductsConfigurations : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(p => p.ProductID);
            builder.HasOne(p => p.Category).WithMany(p => p.Products).HasForeignKey(p => p.CategoryID).HasConstraintName("FK_product_category");
            builder.HasOne(p => p.Brand).WithMany(p => p.Products).HasForeignKey(p => p.BrandID).HasConstraintName("FK_product_brand"); 
            builder.HasMany(p => p.CartItems).WithOne(p => p.Products).HasForeignKey(p => p.ProductId).HasConstraintName("FK_product_CartItems");
            builder.HasMany(p => p.ProductVariants).WithOne(p => p.Products).HasForeignKey(p => p.ProductID).HasConstraintName("FK_product_ProductVariants");
        }
    }
}
