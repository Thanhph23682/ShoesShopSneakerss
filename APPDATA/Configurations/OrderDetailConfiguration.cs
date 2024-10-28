using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APPDATA.Models;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace APPDATA.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(c => c.Id);
            builder.ToTable("OrderDetails");

            builder.HasOne(p => p.order).WithMany(p => p.OrderDetails).HasForeignKey(p => p.orderId).HasConstraintName("FK_OrderDetail_Order");
           




        }
    }
}
