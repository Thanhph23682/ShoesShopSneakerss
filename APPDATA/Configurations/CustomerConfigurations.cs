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
    public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
    {
       

        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");
            builder.HasKey(p => p.Id);
           
            builder.HasMany(p => p.Vouchers).WithOne(p => p.Customer).HasForeignKey(p => p.idCustomer).HasConstraintName("FK_Voucher_Customers");
            builder.HasMany(p => p.Orders).WithOne(p => p.Customer).HasForeignKey(p => p.CustomerId).HasConstraintName("FK_Orders_Customers");
 builder.HasMany(p => p.Carts).WithOne(p => p.Customers).HasForeignKey(p => p.CustomerId).HasConstraintName("Cart_Customers");

        }
    }
}
