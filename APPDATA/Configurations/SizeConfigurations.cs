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
    public class SizeConfigurations : IEntityTypeConfiguration<size>
    {
        public void Configure(EntityTypeBuilder<size> builder)
        {
            builder.ToTable("Size");
            builder.HasKey(p => p.sizeID);
            builder.HasMany(p => p.ProductVariants).WithOne(p => p.size).HasForeignKey(p => p.sizeID).HasConstraintName("FK_productVariant_Size");
        }
    }
}
