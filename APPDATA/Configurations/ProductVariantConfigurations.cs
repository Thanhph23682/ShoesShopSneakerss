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
    public class ProductVariantConfigurations : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {
            builder.ToTable("ProductVariant");
            builder.HasKey(p => p.ProductVariantID);
            builder.HasOne(p => p.color).WithMany(p => p.ProductVariants).HasForeignKey(p => p.colorID).HasConstraintName("FK_product_color");

            builder.HasOne(p => p.size).WithMany(p => p.ProductVariants).HasForeignKey(p => p.sizeID).HasConstraintName("FK_product_size");

            builder.HasOne(p => p.OrderDetail).WithMany(p => p.ProductVariants).HasForeignKey(p => p.ProductVariantID).HasConstraintName("FK_product_size");
        }
    }
}
