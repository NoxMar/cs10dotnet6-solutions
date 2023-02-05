using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Exercise02.Db
{
    public partial class NorthwindContext : DbContext
    {
        public NorthwindContext()
        {
        }

        public NorthwindContext(DbContextOptions<NorthwindContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("Filename=Northwind.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(e => e.City, "City");

                entity.HasIndex(e => e.CompanyName, "CompanyNameCustomers");

                entity.HasIndex(e => e.PostalCode, "PostalCodeCustomers");

                entity.HasIndex(e => e.Region, "Region");

                entity.Property(e => e.CustomerId).HasColumnType("nchar (5)");

                entity.Property(e => e.Address).HasColumnType("nvarchar (60)");

                entity.Property(e => e.City).HasColumnType("nvarchar (15)");

                entity.Property(e => e.CompanyName).HasColumnType("nvarchar (40)");

                entity.Property(e => e.ContactName).HasColumnType("nvarchar (30)");

                entity.Property(e => e.ContactTitle).HasColumnType("nvarchar (30)");

                entity.Property(e => e.Country).HasColumnType("nvarchar (15)");

                entity.Property(e => e.Fax).HasColumnType("nvarchar (24)");

                entity.Property(e => e.Phone).HasColumnType("nvarchar (24)");

                entity.Property(e => e.PostalCode).HasColumnType("nvarchar (10)");

                entity.Property(e => e.Region).HasColumnType("nvarchar (15)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
