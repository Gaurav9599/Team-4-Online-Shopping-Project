using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AdminFinal.Models
{
    public partial class shoppingDB1Context : DbContext
    {
        public shoppingDB1Context()
        {
        }

        public shoppingDB1Context(DbContextOptions<shoppingDB1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<CustomerDetail> CustomerDetails { get; set; } = null!;
        public virtual DbSet<CustomerOrder> CustomerOrders { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<ProductDetail> ProductDetails { get; set; } = null!;
        public virtual DbSet<Retailer> Retailers { get; set; } = null!;
        public virtual DbSet<ShippingAddress> ShippingAddresses { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=NAGENDRA\\SQLEXPRESS;Database=shoppingDB1;user id=sa;password=newuser123#;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.Property(e => e.AdminEmail).HasMaxLength(100);

                entity.Property(e => e.AdminName).HasMaxLength(100);

                entity.Property(e => e.AdminPhoneNum).HasMaxLength(200);

                entity.Property(e => e.Adminpassword).HasMaxLength(100);
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("Cart");

                entity.Property(e => e.CustomerId).HasColumnName("customerId");

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Cart__customerId__412EB0B6");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Cart__productId__4222D4EF");
            });

            modelBuilder.Entity<CustomerDetail>(entity =>
            {
                entity.HasKey(e => e.CustId)
                    .HasName("PK__Customer__049E3AA950FAFEEB");

                entity.Property(e => e.CustName).HasMaxLength(100);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNum).HasMaxLength(100);
            });

            modelBuilder.Entity<CustomerOrder>(entity =>
            {
                entity.HasKey(e => e.CustOrderId)
                    .HasName("PK__Customer__8347B4734C05C819");

                entity.ToTable("CustomerOrder");

                entity.Property(e => e.CustOrderId).HasColumnName("custOrderId");

                entity.Property(e => e.CustomerId).HasColumnName("customerId");

                entity.Property(e => e.Orderdate)
                    .HasColumnType("date")
                    .HasColumnName("orderdate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.PurchasingQuantity).HasColumnName("purchasingQuantity");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerOrders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__CustomerO__custo__46E78A0C");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CustomerOrders)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__CustomerO__produ__45F365D3");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.CustOrderId).HasColumnName("custOrderId");

                entity.HasOne(d => d.CustOrder)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustOrderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Orders__custOrde__5629CD9C");
            });

            modelBuilder.Entity<ProductDetail>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK__productD__2D10D16ACB78DFC4");

                entity.ToTable("productDetails");

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.Fabric)
                    .HasMaxLength(100)
                    .HasColumnName("fabric");

                entity.Property(e => e.Images)
                    .HasMaxLength(200)
                    .HasColumnName("images");

                entity.Property(e => e.ProductBrand)
                    .HasMaxLength(100)
                    .HasColumnName("productBrand");

                entity.Property(e => e.ProductCategory)
                    .HasMaxLength(100)
                    .HasColumnName("productCategory");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(100)
                    .HasColumnName("productName");

                entity.Property(e => e.ProductPrice)
                    .HasColumnType("money")
                    .HasColumnName("productPrice");

                entity.Property(e => e.ProductQuantity).HasColumnName("productQuantity");

                entity.Property(e => e.Ratings).HasColumnName("ratings");

                entity.Property(e => e.RetailerId).HasColumnName("retailerId");

                entity.HasOne(d => d.Retailer)
                    .WithMany(p => p.ProductDetails)
                    .HasForeignKey(d => d.RetailerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__productDe__retai__3E52440B");
            });

            modelBuilder.Entity<Retailer>(entity =>
            {
                entity.ToTable("retailer");

                entity.Property(e => e.RetailerId).HasColumnName("retailerId");

                entity.Property(e => e.ApprovedStatus)
                    .HasColumnName("approvedStatus")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.City)
                    .HasMaxLength(100)
                    .HasColumnName("city");

                entity.Property(e => e.Country)
                    .HasMaxLength(100)
                    .HasColumnName("country");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .HasColumnName("password");

                entity.Property(e => e.Pincode).HasColumnName("pincode");

                entity.Property(e => e.ProductType)
                    .HasMaxLength(100)
                    .HasColumnName("productType");

                entity.Property(e => e.RetailerEmail)
                    .HasMaxLength(200)
                    .HasColumnName("retailerEmail");

                entity.Property(e => e.RetailerName)
                    .HasMaxLength(100)
                    .HasColumnName("retailerName");

                entity.Property(e => e.RetailerPhoneNum)
                    .HasMaxLength(100)
                    .HasColumnName("retailerPhoneNum");

                entity.Property(e => e.State)
                    .HasMaxLength(100)
                    .HasColumnName("state");
            });

            modelBuilder.Entity<ShippingAddress>(entity =>
            {
                entity.HasKey(e => e.AddressId)
                    .HasName("PK__Shipping__091C2AFB5AE92CFB");

                entity.ToTable("ShippingAddress");

                entity.Property(e => e.City)
                    .HasMaxLength(100)
                    .HasColumnName("city");

                entity.Property(e => e.Landmark)
                    .HasMaxLength(100)
                    .HasColumnName("landmark");

                entity.Property(e => e.Pincode).HasColumnName("pincode");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Village)
                    .HasMaxLength(100)
                    .HasColumnName("village");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ShippingAddresses)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__ShippingA__userI__38996AB5");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
