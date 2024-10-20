using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using APPDATA.Models;

namespace APPDATA.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable  ("User");
            // Mối quan hệ với Role
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Role)
                   .WithOne() 
                   .HasForeignKey<User>(u => u.RoleId) 
                   .HasConstraintName("FK_User_Role");
        }
    }
}
