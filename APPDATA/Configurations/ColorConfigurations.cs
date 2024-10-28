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
    public class ColorConfigurations : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.ToTable("Color");
            builder.HasKey(p => p.colorID);
            builder.HasMany(p => p.ProductVariants).WithOne(p => p.color).HasForeignKey(p => p.colorID).HasConstraintName("FK_productVariant_Color");
        }
    }
}
