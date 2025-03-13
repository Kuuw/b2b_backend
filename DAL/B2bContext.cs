using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Entities.Models;

namespace DAL;

public partial class B2bContext : DbContext
{
    public B2bContext()
    {
    }

    public B2bContext(DbContextOptions<B2bContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<AddressType> AddressTypes { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<DiscountType> DiscountTypes { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<LogType> LogTypes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductStock> ProductStocks { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=b2b;Integrated Security=true;Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.Property(e => e.AddressId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("AddressID");
            entity.Property(e => e.AddressTypeId).HasColumnName("AddressTypeID");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Street)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.AddressType).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.AddressTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Addresses_AddressTypeID");

            entity.HasOne(d => d.Country).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Addresses_CountryID");

            entity.HasOne(d => d.User).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Addresses_UserID");
        });

        modelBuilder.Entity<AddressType>(entity =>
        {
            entity.HasIndex(e => e.TypeName, "UQ_AddressTypes_TypeName").IsUnique();

            entity.Property(e => e.AddressTypeId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("AddressTypeID");
            entity.Property(e => e.TypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasIndex(e => e.CategoryName, "UQ_Categories_CategoryName").IsUnique();

            entity.Property(e => e.CategoryId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasIndex(e => e.CompanyName, "UQ_Companies_CompanyName").IsUnique();

            entity.Property(e => e.CompanyId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("CompanyID");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasIndex(e => e.CountryCode, "UQ_Countries_CountryCode").IsUnique();

            entity.Property(e => e.CountryId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("CountryID");
            entity.Property(e => e.CountryCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CountryName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.Property(e => e.DiscountId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("DiscountID");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DiscountTypeId).HasColumnName("DiscountTypeID");
            entity.Property(e => e.DiscountValue).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.DiscountType).WithMany(p => p.Discounts)
                .HasForeignKey(d => d.DiscountTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Discounts_DiscountTypeID");
        });

        modelBuilder.Entity<DiscountType>(entity =>
        {
            entity.HasIndex(e => e.DiscountTypeName, "UQ_DiscountTypes_DiscountTypeName").IsUnique();

            entity.Property(e => e.DiscountTypeId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("DiscountTypeID");
            entity.Property(e => e.DiscountTypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasIndex(e => e.InvoiceNumber, "UQ_Invoices_InvoiceNumber").IsUnique();

            entity.Property(e => e.InvoiceId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("InvoiceID");
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.InvoiceDate).HasColumnType("datetime");
            entity.Property(e => e.InvoiceNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Order).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoices_OrderID");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.Property(e => e.LogId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("LogID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LogMessage)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.LogTypeId).HasColumnName("LogTypeID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.LogType).WithMany(p => p.Logs)
                .HasForeignKey(d => d.LogTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Logs_LogTypeID");

            entity.HasOne(d => d.User).WithMany(p => p.Logs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Logs_UserID");
        });

        modelBuilder.Entity<LogType>(entity =>
        {
            entity.HasIndex(e => e.LogTypeName, "UQ_LogTypes_LogTypeName").IsUnique();

            entity.Property(e => e.LogTypeId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("LogTypeID");
            entity.Property(e => e.LogTypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.OrderId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("OrderID");
            entity.Property(e => e.DiscountId).HasColumnName("DiscountID");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Discount).WithMany(p => p.Orders)
                .HasForeignKey(d => d.DiscountId)
                .HasConstraintName("FK_Orders_DiscountID");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_StatusID");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_UserID");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.Property(e => e.OrderItemId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("OrderItemID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItems_OrderID");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItems_ProductID");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.Property(e => e.PaymentId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("PaymentID");
            entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");
            entity.Property(e => e.PaymentAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Invoice).WithMany(p => p.Payments)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payments_InvoiceID");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasIndex(e => e.PermissionName, "UQ_Permissions_PermissionName").IsUnique();

            entity.Property(e => e.PermissionId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("PermissionID");
            entity.Property(e => e.PermissionName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(e => e.ProductName, "UQ_Products_ProductName").IsUnique();

            entity.Property(e => e.ProductId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ProductID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductDescription).IsUnicode(false);
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_CategoryID");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.ToTable("ProductImage");

            entity.Property(e => e.ProductImageId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ProductImageID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductImage_ProductID");
        });

        modelBuilder.Entity<ProductStock>(entity =>
        {
            entity.Property(e => e.ProductStockId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ProductStockID");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductStocks)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductStocks_ProductID");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasIndex(e => e.RoleName, "UQ_Roles_RoleName").IsUnique();

            entity.Property(e => e.RoleId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("RoleID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasMany(d => d.Permissions).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolePermission",
                    r => r.HasOne<Permission>().WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_RolePermissions_PermissionID"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_RolePermissions_RoleID"),
                    j =>
                    {
                        j.HasKey("RoleId", "PermissionId");
                        j.ToTable("RolePermissions");
                        j.IndexerProperty<Guid>("RoleId").HasColumnName("RoleID");
                        j.IndexerProperty<Guid>("PermissionId").HasColumnName("PermissionID");
                    });
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasIndex(e => e.StatusName, "UQ_Statuses_StatusName").IsUnique();

            entity.Property(e => e.StatusId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("StatusID");
            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Email, "UQ_Users_Email").IsUnique();

            entity.Property(e => e.UserId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("UserID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Company).WithMany(p => p.Users)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK_Users_Companies");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
