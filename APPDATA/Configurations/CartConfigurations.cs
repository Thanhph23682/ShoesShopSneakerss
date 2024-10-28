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
    public class CartConfigurations : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Cart");
            builder.HasKey(p => p.Id);
            // Mối quan hệ với Customer
            builder.HasOne(p => p.Customers).WithMany(p => p.Carts).HasForeignKey(p => p.CustomerId).HasConstraintName("FK_Cart_Customer");
            // Mối quan hệ với CartItems
            
            builder.HasMany(p => p.CartItems).WithOne(p => p.Cart).HasForeignKey(p => p.Id).HasConstraintName("FK_cart_CartItems");
        }
    }
}
