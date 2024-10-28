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
    public class RoleConfigurations : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");
            builder.HasKey(p => p.ID);
            // Mối quan hệ với Users
            builder.HasMany(p => p.Users).WithOne(p => p.Role).HasForeignKey(p => p.RoleID).HasConstraintName("FK_User_Role");
            // Mối quan hệ với Customers
            builder.HasMany(p => p.Customers).WithOne(p => p.Role).HasForeignKey(p => p.RoleId).HasConstraintName("FK_Customer_Role");
        }
    }
}
