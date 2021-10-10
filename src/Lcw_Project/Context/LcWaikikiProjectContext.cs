using System;
using Lcw_Project.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Lcw_Project.Context
{
    public partial class LcWaikikiProjectContext : DbContext
    {
        public LcWaikikiProjectContext()
        {
        }

        public LcWaikikiProjectContext(DbContextOptions<LcWaikikiProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LcwCustomer> LcwCustomers { get; set; }
        public virtual DbSet<LcwCustomerOrder> LcwCustomerOrders { get; set; }
        public virtual DbSet<LcwCustomerOrderItem> LcwCustomerOrderItems { get; set; }
        public virtual DbSet<LcwProduct> LcwProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=DESKTOP-G2TU039;Database=LcWaikikiProject;User Id=sa;Password=s;");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_CS_AS");

            modelBuilder.Entity<LcwCustomer>(entity =>
            {
                entity.ToTable("Lcw_Customer");

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CustomerSurname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<LcwCustomerOrder>(entity =>
            {
                entity.ToTable("Lcw_CustomerOrder");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.LcwCustomerOrders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Lcw_CustomerOrder_Lcw_Customer");
            });

            modelBuilder.Entity<LcwCustomerOrderItem>(entity =>
            {
                entity.ToTable("Lcw_CustomerOrderItem");

                entity.HasOne(d => d.CustomerOrder)
                    .WithMany(p => p.LcwCustomerOrderItems)
                    .HasForeignKey(d => d.CustomerOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Lcw_CustomerOrderItem_Lcw_CustomerOrder");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.LcwCustomerOrderItems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Lcw_CustomerOrderItem_Lcw_Product");
            });

            modelBuilder.Entity<LcwProduct>(entity =>
            {
                entity.ToTable("Lcw_Product");

                entity.Property(e => e.Barcode)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
