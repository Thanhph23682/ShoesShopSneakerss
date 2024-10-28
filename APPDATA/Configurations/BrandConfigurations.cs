using APPDATA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPDATA.Configurations
{
    public class BrandConfigurations : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("Brand");
            builder.HasKey(p => p.BrandId);
            builder.HasMany(p => p.Products).WithOne(p => p.Brand).HasForeignKey(p => p.BrandID).HasConstraintName("FK_ThuongHieu_SanPham");
        }
    }
}
