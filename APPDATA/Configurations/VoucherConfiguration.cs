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
    public class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.HasKey(c => c.ID);
            builder.ToTable("Voucher");
            builder.HasMany(p => p.VoucherDetails).WithOne(p => p.Voucher).HasForeignKey(p => p.VoucherID).HasConstraintName("FK_voucher_voucherDetails");
        }
    }
}
