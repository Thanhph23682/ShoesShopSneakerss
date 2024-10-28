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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(c => c.Id);
            builder.ToTable("Order");
            builder.HasMany(p => p.Payments).WithOne(p => p.Order).HasForeignKey(p => p.PaymentID).HasConstraintName("FK_Payment_Orders");
            builder.HasMany(p => p.OrderDetails).WithOne(p => p.Order).HasForeignKey(p => p.OrderId).HasConstraintName("FK_OrderDetails_Orders");
            builder.HasOne(p => p.Customer).WithMany(p => p.Orders).HasForeignKey(p => p.CustomerId).HasConstraintName("FK_Customer_Orders").OnDelete(DeleteBehavior.Cascade);
        }
    }
}
