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
            //builder.HasOne(c => c.User).WithMany(c => c.Order).HasForeignKey(c => c.Userid);
            //builder.HasOne(c => c.Customer).WithMany(c => c.Order).HasForeignKey(c => c.CustomerId);
        }
    }
}
