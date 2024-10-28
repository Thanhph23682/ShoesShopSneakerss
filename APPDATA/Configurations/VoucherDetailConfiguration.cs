using APPDATA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace APPDATA.Configurations
{
    public class VoucherDetailConfiguration : IEntityTypeConfiguration<VoucherDetail>
    {
        public void Configure(EntityTypeBuilder<VoucherDetail> builder)
        {
            builder.ToTable("VoucherDetails");
            builder.HasKey(c => c.ID);
            //builder.HasOne(p => p.Role)
            //               .WithMany(p => p.Users)
            //               .HasForeignKey(p => p.RoleId)
            //               .HasConstraintName("FK_User_Role");
            builder.HasOne(p => p.Voucher).WithMany(p => p.VoucherDetails).HasForeignKey
                (p => p.VoucherID).HasConstraintName("FK_VoucherDetail_Voucher");
        } 
    }
}
