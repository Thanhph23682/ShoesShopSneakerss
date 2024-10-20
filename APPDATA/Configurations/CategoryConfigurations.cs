﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APPDATA.Models;

namespace APPDATA.Configurations
{
    public class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");
            builder.HasKey(p => p.CategoryID);  // Thiết lập pk
            builder.HasMany(p => p.products).WithOne(p => p.category).HasForeignKey(p => p.categoryID).HasConstraintName("FK_sanpham_danhmuc");
        }
    }
   
}