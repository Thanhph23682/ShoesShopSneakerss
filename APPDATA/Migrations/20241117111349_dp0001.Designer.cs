﻿// <auto-generated />
using System;
using APPDATA.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace APPDATA.Migrations
{
    [DbContext(typeof(ShopDbContext))]
    [Migration("20241117111349_dp0001")]
    partial class dp0001
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.36")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("APPDATA.Models.Brand", b =>
                {
                    b.Property<int>("BrandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BrandId"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BrandId");

                    b.ToTable("Brand", (string)null);
                });

            modelBuilder.Entity("APPDATA.Models.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Cart", (string)null);
                });

            modelBuilder.Entity("APPDATA.Models.CartItems", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<decimal>("PriceAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartItems", (string)null);
                });

            modelBuilder.Entity("APPDATA.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryID"), 1L, 1);

                    b.Property<string>("Alias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Images")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("CategoryID");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("APPDATA.Models.Color", b =>
                {
                    b.Property<int>("ColorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ColorID"), 1L, 1);

                    b.Property<string>("ColorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ColorValue")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ColorID");

                    b.ToTable("Color", (string)null);
                });

            modelBuilder.Entity("APPDATA.Models.Contact", b =>
                {
                    b.Property<int>("ContactID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContactID"), 1L, 1);

                    b.Property<string>("ContactDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ContactID");

                    b.ToTable("Contact", (string)null);
                });

            modelBuilder.Entity("APPDATA.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("BirthDay")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Customer", (string)null);
                });

            modelBuilder.Entity("APPDATA.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Order_Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Order_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Order_Desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Order_Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Order_Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Order_Status")
                        .HasColumnType("int");

                    b.Property<int>("Payment_Status")
                        .HasColumnType("int");

                    b.Property<decimal?>("Total_Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("Update_Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("UserId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("APPDATA.Models.OrderDetail", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<decimal>("AmountPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetails", (string)null);
                });

            modelBuilder.Entity("APPDATA.Models.Payment", b =>
                {
                    b.Property<int>("PaymentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentID"), 1L, 1);

                    b.Property<int>("OrderID")
                        .HasColumnType("int");

                    b.Property<string>("PaymentDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PaymentImage")
                        .HasColumnType("int");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaymentID");

                    b.HasIndex("OrderID");

                    b.ToTable("Payment", (string)null);
                });

            modelBuilder.Entity("APPDATA.Models.Products", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductID"), 1L, 1);

                    b.Property<string>("Alias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BrandID")
                        .HasColumnType("int");

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Desciption")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameProduct")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("Thumbnail")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductID");

                    b.HasIndex("BrandID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("APPDATA.Models.ProductVariant", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("ColorID")
                        .HasColumnType("int");

                    b.Property<int>("OrderDetailID")
                        .HasColumnType("int");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("SizeID")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdateUser")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ColorID");

                    b.HasIndex("OrderDetailID");

                    b.HasIndex("ProductID");

                    b.HasIndex("SizeID");

                    b.ToTable("ProductVariant", (string)null);
                });

            modelBuilder.Entity("APPDATA.Models.Role", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("APPDATA.Models.Size", b =>
                {
                    b.Property<int>("SizeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SizeID"), 1L, 1);

                    b.Property<int>("NameSize")
                        .HasColumnType("int");

                    b.HasKey("SizeID");

                    b.ToTable("Size", (string)null);
                });

            modelBuilder.Entity("APPDATA.Models.Slider", b =>
                {
                    b.Property<int>("SliderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SliderID"), 1L, 1);

                    b.Property<string>("SliderImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SliderStatus")
                        .HasColumnType("bit");

                    b.HasKey("SliderID");

                    b.ToTable("Slider", (string)null);
                });

            modelBuilder.Entity("APPDATA.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("RoleID");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("APPDATA.Models.Voucher", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Conditions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PercentDiscount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Voucher", (string)null);
                });

            modelBuilder.Entity("APPDATA.Models.VoucherDetail", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("VoucherID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("VoucherID");

                    b.ToTable("VoucherDetails", (string)null);
                });

            modelBuilder.Entity("APPDATA.Models.Cart", b =>
                {
                    b.HasOne("APPDATA.Models.Customer", "Customers")
                        .WithMany("Carts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("Cart_Customers");

                    b.Navigation("Customers");
                });

            modelBuilder.Entity("APPDATA.Models.CartItems", b =>
                {
                    b.HasOne("APPDATA.Models.Cart", "Cart")
                        .WithMany("CartItems")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_cart_CartItems");

                    b.HasOne("APPDATA.Models.Products", "Products")
                        .WithMany("CartItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_product_CartItems");

                    b.Navigation("Cart");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("APPDATA.Models.Customer", b =>
                {
                    b.HasOne("APPDATA.Models.Role", "Role")
                        .WithMany("Customers")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Customer_Role");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("APPDATA.Models.Order", b =>
                {
                    b.HasOne("APPDATA.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Customer_Orders");

                    b.HasOne("APPDATA.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_user_Order");

                    b.Navigation("Customer");

                    b.Navigation("User");
                });

            modelBuilder.Entity("APPDATA.Models.OrderDetail", b =>
                {
                    b.HasOne("APPDATA.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_OrderDetail_Order");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("APPDATA.Models.Payment", b =>
                {
                    b.HasOne("APPDATA.Models.Order", "Order")
                        .WithMany("Payments")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("Fk_Payment_Order");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("APPDATA.Models.Products", b =>
                {
                    b.HasOne("APPDATA.Models.Brand", "Brand")
                        .WithMany("Products")
                        .HasForeignKey("BrandID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_product_brand");

                    b.HasOne("APPDATA.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_product_category");

                    b.Navigation("Brand");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("APPDATA.Models.ProductVariant", b =>
                {
                    b.HasOne("APPDATA.Models.Color", "Color")
                        .WithMany("ProductVariants")
                        .HasForeignKey("ColorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_product_color");

                    b.HasOne("APPDATA.Models.OrderDetail", "OrderDetail")
                        .WithMany("ProductVariants")
                        .HasForeignKey("OrderDetailID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_product_orderDetail");

                    b.HasOne("APPDATA.Models.Products", "Products")
                        .WithMany("ProductVariants")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_product_ProductVariants");

                    b.HasOne("APPDATA.Models.Size", "Size")
                        .WithMany("ProductVariants")
                        .HasForeignKey("SizeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_productVariant_Size");

                    b.Navigation("Color");

                    b.Navigation("OrderDetail");

                    b.Navigation("Products");

                    b.Navigation("Size");
                });

            modelBuilder.Entity("APPDATA.Models.User", b =>
                {
                    b.HasOne("APPDATA.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_User_Role");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("APPDATA.Models.Voucher", b =>
                {
                    b.HasOne("APPDATA.Models.Customer", "Customer")
                        .WithMany("Vouchers")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Voucher_Customers");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("APPDATA.Models.VoucherDetail", b =>
                {
                    b.HasOne("APPDATA.Models.Voucher", "Voucher")
                        .WithMany("VoucherDetails")
                        .HasForeignKey("VoucherID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_VoucherDetail_Voucher");

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("APPDATA.Models.Brand", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("APPDATA.Models.Cart", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("APPDATA.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("APPDATA.Models.Color", b =>
                {
                    b.Navigation("ProductVariants");
                });

            modelBuilder.Entity("APPDATA.Models.Customer", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("Orders");

                    b.Navigation("Vouchers");
                });

            modelBuilder.Entity("APPDATA.Models.Order", b =>
                {
                    b.Navigation("OrderDetails");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("APPDATA.Models.OrderDetail", b =>
                {
                    b.Navigation("ProductVariants");
                });

            modelBuilder.Entity("APPDATA.Models.Products", b =>
                {
                    b.Navigation("CartItems");

                    b.Navigation("ProductVariants");
                });

            modelBuilder.Entity("APPDATA.Models.Role", b =>
                {
                    b.Navigation("Customers");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("APPDATA.Models.Size", b =>
                {
                    b.Navigation("ProductVariants");
                });

            modelBuilder.Entity("APPDATA.Models.User", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("APPDATA.Models.Voucher", b =>
                {
                    b.Navigation("VoucherDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
