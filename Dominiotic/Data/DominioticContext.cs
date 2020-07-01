using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Dominiotic.Entities.Models
{
    public partial class DominioticContext : DbContext
    {
        public DominioticContext()
        {
        }

        public DominioticContext(DbContextOptions<DominioticContext> options) : base(options)
        {
        }

        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Establishment> Establishment { get; set; }
        public virtual DbSet<Ingredients> Ingredients { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OrdersItems> OrdersItems { get; set; }
        public virtual DbSet<Plates> Plates { get; set; }
        public virtual DbSet<PlatesItems> PlatesItems { get; set; }
        public virtual DbSet<Receivables> Receivables { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //                 #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer("Data Source=WINDOWS-S291RF5;Initial Catalog=FoodONE;Integrated Security=True");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clients>(entity =>
            {
                entity.HasKey(e => e.ClientId);

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Clients__A9D105348570B78C")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Identifier).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.Registered)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Establishment>(entity =>
            {
                entity.HasKey(e => e.CompanyId);

                entity.Property(e => e.CoAddress)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("EMail")
                    .HasMaxLength(128);

                entity.Property(e => e.Fax)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Identifier).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Phone2)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Registered)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.WebSite)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<Ingredients>(entity =>
            {
                entity.HasKey(e => e.IngredientId);

                entity.Property(e => e.Identifier).HasDefaultValueSql("(newid())");

                entity.Property(e => e.IngredientName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Registered)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(10, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Identifier).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Registered)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders__ClientId__44FF419A");
            });

            modelBuilder.Entity<OrdersItems>(entity =>
            {
                entity.HasKey(e => e.ItemId);

                entity.Property(e => e.Identifier).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Registered)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.OrdersItems)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrdersIte__Ingre__4BAC3F29");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrdersItems)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrdersIte__Order__4AB81AF0");
            });

            modelBuilder.Entity<Plates>(entity =>
            {
                entity.HasKey(e => e.PlateId);

                entity.Property(e => e.Identifier).HasDefaultValueSql("(newid())");

                entity.Property(e => e.PlateName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Registered)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<PlatesItems>(entity =>
            {
                entity.HasKey(e => e.ItemId);

                entity.Property(e => e.Identifier).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Registered)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.PlatesItems)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PlatesIte__Ingre__5629CD9C");

                entity.HasOne(d => d.Plate)
                    .WithMany(p => p.PlatesItems)
                    .HasForeignKey(d => d.PlateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PlatesIte__Plate__5535A963");
            });

            modelBuilder.Entity<Receivables>(entity =>
            {
                entity.HasKey(e => e.ReceivableId);

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(10, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DueDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Identifier).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Registered)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Receivables)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Receivabl__Clien__5AEE82B9");
            });
        }
    }
}
