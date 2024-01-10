
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using SupplyManagement.Models;

namespace SupplyManagement.Data
{
    public class SMDbContext : DbContext
    {
        // Add a constructor that takes a connection string or connection string name
        public SMDbContext()
             : base("name=DefaultConnection") // Asumsikan "DefaultConnection" adalah nama connection string Anda
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Vendor> Vendors { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>()
                .HasMany(c => c.Vendors)  // Company memiliki banyak Vendor
                .WithRequired(v => v.Company)  // Vendor memerlukan Company
                .HasForeignKey(v => v.CompanyGuid);  // Foreign key di Vendor untuk Company


            // Contoh konfigurasi relasi opsional satu-ke-satu antara Company dan Account
            modelBuilder.Entity<Company>()
                .HasOptional(a => a.Account)
                .WithRequired(c => c.Company);

            // Contoh konfigurasi relasi satu-ke-banyak antara Role dan Account
            modelBuilder.Entity<Role>()
                .HasMany(r => r.Accounts)
                .WithRequired(a => a.Role)
                .HasForeignKey(a => a.RoleGuid);

            // Konfigurasi lainnya...
        }
    }
}
