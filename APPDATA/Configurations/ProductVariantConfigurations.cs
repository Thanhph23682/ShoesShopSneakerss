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
            builder.HasKey(p => p.ID);
            builder.HasOne(p => p.Color).WithMany(p => p.ProductVariants).HasForeignKey(p => p.ColorID).HasConstraintName("FK_product_color");
            builder.HasOne(p => p.Size).WithMany(p => p.ProductVariants).HasForeignKey(p => p.SizeID).HasConstraintName("FK_product_size");
            builder.HasOne(p => p.OrderDetail).WithMany(p => p.ProductVariants).HasForeignKey(p => p.OrderDetailID).HasConstraintName("FK_product_orderDetail");
        }
    }
}
