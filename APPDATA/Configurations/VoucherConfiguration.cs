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
            builder.HasKey(c => c.Id);
   builder.ToTable("Voucher");
            builder.HasMany(p => p.voucherDetails).WithOne(p => p.voucher).HasForeignKey(p => p.idVoucher).HasConstraintName("FK_voucher_voucherDetails");
        }
    }
}
