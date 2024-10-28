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
            builder.HasKey(p => p.ColorID);
            builder.HasMany(p => p.ProductVariants).WithOne(p => p.Color).HasForeignKey(p => p.ColorID).HasConstraintName("FK_productVariant_Color");
        }
    }
}
