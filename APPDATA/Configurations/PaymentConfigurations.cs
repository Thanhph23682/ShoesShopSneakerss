using APPDATA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APPDATA.Configurations
{
    public class PaymentConfigurations : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            // Đặt tên bảng cho thực thể Payment
            builder.ToTable("Payment");

            // Xác định khóa chính cho bảng Payment
            builder.HasKey(p => p.PaymentID);


            // Thiết lập quan hệ một-một giữa Payment và Order
            builder.HasOne(p => p.Order)
                   .WithMany(p => p.Payments)
                   .HasForeignKey(p => p.PaymentID)  // Khóa ngoại PaymentID
                   .HasConstraintName("FK_Payment_Orders")
                   .OnDelete(DeleteBehavior.Cascade);  // Xóa Payment khi Order bị xóa
        }
    }
}
