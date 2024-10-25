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
            // Mối quan hệ với Role
            builder.HasKey(p => p.Id);
            //builder.HasOne(p => p.Role) 
            //      .WithOne() 
            //      .HasForeignKey<Customer>(p => p.RoleId)
            //      .HasConstraintName("FK_Customer_Role");
        }
    }
}
