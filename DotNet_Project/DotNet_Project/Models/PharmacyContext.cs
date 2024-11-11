using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DotNet_Project.Models;

public partial class PharmacyContext : DbContext
{
    public PharmacyContext()
    {
    }

    public PharmacyContext(DbContextOptions<PharmacyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApplyJob> ApplyJobs { get; set; }

    public virtual DbSet<Career> Careers { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Quote> Quotes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Wishlist> Wishlists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=DESKTOP-22TIADC\\SQLEXPRESS; initial catalog=Pharmacy; Trusted_Connection=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplyJob>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ApplyJob__3214EC07D6372344");

            entity.ToTable("ApplyJob");

            entity.Property(e => e.Resume).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(255);

            entity.HasOne(d => d.Job).WithMany(p => p.ApplyJobs)
                .HasForeignKey(d => d.Jobid)
                .HasConstraintName("FK_ApplyJob_ToJobs");

            entity.HasOne(d => d.User).WithMany(p => p.ApplyJobs)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("FK_ApplyJob_ToLogin");
        });

        modelBuilder.Entity<Career>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Career__3214EC07BEAF9C79");

            entity.ToTable("Career");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.Country).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Number).HasMaxLength(255);
            entity.Property(e => e.Qualification).HasMaxLength(255);
            entity.Property(e => e.Resume).HasMaxLength(255);
            entity.Property(e => e.YearOfQualification)
                .HasMaxLength(255)
                .HasColumnName("Year_of_Qualification");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__cart__3214EC0773F8DE88");

            entity.ToTable("cart");

            entity.Property(e => e.Proid).HasColumnName("proid");
            entity.Property(e => e.Proprice).HasColumnName("proprice");
            entity.Property(e => e.Qty).HasColumnName("qty");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Pro).WithMany(p => p.Carts)
                .HasForeignKey(d => d.Proid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cart_ToTable");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cart_ToTable_1");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC078A19415E");

            entity.ToTable("Category");

            entity.Property(e => e.Image).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Company__3214EC07374A09DD");

            entity.ToTable("Company");

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.Logo).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Number).HasMaxLength(255);
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Contact__3214EC07E651DB5B");

            entity.ToTable("Contact");

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FName)
                .HasMaxLength(255)
                .HasColumnName("F_Name");
            entity.Property(e => e.LName)
                .HasMaxLength(255)
                .HasColumnName("L_Name");
            entity.Property(e => e.Message).HasColumnType("text");
            entity.Property(e => e.Number).HasMaxLength(255);
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Jobs__3214EC071674EC39");

            entity.Property(e => e.ApplicationDateTime)
                .HasMaxLength(255)
                .HasColumnName("Application_DateTime");
            entity.Property(e => e.Department).HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.EmpType)
                .HasMaxLength(255)
                .HasColumnName("Emp_Type");
            entity.Property(e => e.Experience).HasMaxLength(255);
            entity.Property(e => e.Image).HasMaxLength(255);
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.Qualification).HasMaxLength(255);
            entity.Property(e => e.Salary).HasMaxLength(255);
            entity.Property(e => e.Skills).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Login__3214EC077C38A042");

            entity.ToTable("Login");

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Image).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.RoleId).HasColumnName("Role_Id");

            entity.HasOne(d => d.Role).WithMany(p => p.Logins)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Login_ToTable");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__order__3214EC0726AB4503");

            entity.ToTable("order");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(50)
                .HasColumnName("customer_name");
            entity.Property(e => e.Date)
                .HasMaxLength(50)
                .HasColumnName("date");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('Pending')");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .HasColumnName("phone_number");
            entity.Property(e => e.TotalAmount).HasColumnName("total_amount");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("FK_order_ToTable");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__order_de__3214EC07AF97422A");

            entity.ToTable("order_detail");

            entity.Property(e => e.Date)
                .HasMaxLength(50)
                .HasColumnName("date");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ProId).HasColumnName("pro_id");
            entity.Property(e => e.ProName)
                .HasMaxLength(50)
                .HasColumnName("pro_name");
            entity.Property(e => e.Proprice).HasColumnName("proprice");
            entity.Property(e => e.Qty).HasColumnName("qty");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_order_detail_ToTable_2");

            entity.HasOne(d => d.Pro).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProId)
                .HasConstraintName("FK_order_detail_ToTable");

            entity.HasOne(d => d.User).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("FK_order_detail_ToTable_1");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC0718575EB5");

            entity.ToTable("Product");

            entity.Property(e => e.CId).HasColumnName("C_id");
            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.Image).HasMaxLength(250);
            entity.Property(e => e.Name).HasMaxLength(250);
            entity.Property(e => e.Strength).HasMaxLength(250);

            entity.HasOne(d => d.CIdNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.CId)
                .HasConstraintName("FK_Product_ToTable");
        });

        modelBuilder.Entity<Quote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Quote__3214EC071D05C95D");

            entity.ToTable("Quote");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.Comments).HasColumnType("text");
            entity.Property(e => e.Country).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC07D5CEB97C");

            entity.ToTable("Role");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Wishlist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Wishlist__3214EC0777314C43");

            entity.ToTable("Wishlist");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Image).HasMaxLength(255);
            entity.Property(e => e.Price).HasMaxLength(255);
            entity.Property(e => e.ProId).HasColumnName("Pro_Id");
            entity.Property(e => e.ProName)
                .HasMaxLength(255)
                .HasColumnName("Pro_Name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
