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
            //builder.HasOne(c=>c.Customer).WithMany(c =>c.Voucher).HasForeignKey(c=>c.idCustomer);
            //builder.HasOne(c => c.User).WithMany(c => c.Voucher).HasForeingKey(c => c.idUser);
        }
    }
}
