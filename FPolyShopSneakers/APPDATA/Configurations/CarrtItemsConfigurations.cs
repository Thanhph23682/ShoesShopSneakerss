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
    public class CarrtItemsConfigurations : IEntityTypeConfiguration<CartItems>
    {
        public void Configure(EntityTypeBuilder<CartItems> builder)
        {
            builder.ToTable("CartItems"); 
            builder.HasKey(p => p.Id); 

            // Mối quan hệ với Cart
            builder.HasOne(p => p.Cart) 
                   .WithMany(p => p.CartItems) 
                   .HasForeignKey(p => p.CartId) 
                   
                   .HasConstraintName("FK_CartItems_Cart");
        }
    }
}
