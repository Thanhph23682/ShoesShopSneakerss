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
    public class SizeConfigurations : IEntityTypeConfiguration<Size>
    {
        public void Configure(EntityTypeBuilder<Size> builder)
        {
            builder.ToTable("Size");
            builder.HasKey(p => p.SizeID);
            builder.HasMany(p => p.ProductVariants).WithOne(p => p.Size).HasForeignKey(p => p.SizeID).HasConstraintName("FK_productVariant_Size");
        }
    }
}
