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
            builder.HasMany(o => o.Payments)  // 'Payments' ở đây là danh sách trong Order
                .WithOne(p => p.Order)  // Mỗi Payment chỉ liên kết với một Order
                .HasForeignKey(p => p.PaymentID) // Khóa ngoại là OrderID
                .HasConstraintName("FK_Payment_Orders");

            builder.HasMany(p => p.OrderDetails).WithOne(p => p.Order).HasForeignKey(p => p.OrderId).HasConstraintName("FK_OrderDetails_Orders");
            builder.HasOne(p => p.Customer).WithMany(p => p.Orders).HasForeignKey(p => p.CustomerId).HasConstraintName("FK_Customer_Orders").OnDelete(DeleteBehavior.Cascade);
        }
    }
}
