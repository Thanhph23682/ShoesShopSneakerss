using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using APPDATA.Models;

namespace APPDATA.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable  ("User");
            builder.HasMany(p => p.Orders).WithOne(p => p.User).HasForeignKey(p => p.UserId).HasConstraintName("FK_user_Order").OnDelete(DeleteBehavior.NoAction);
        }
    }
}
