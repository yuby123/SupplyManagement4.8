/*using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using SupplyManagement.Models;
using SupplyManagement.Utilities.Handler;
using SupplyManagement.Utilities.Enums;

namespace SupplyManagement.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SMDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SMDbContext context)
        {
            // Contoh seed untuk Role
            var roleIdVendor = Guid.NewGuid();
            var roleIdAdmin = Guid.NewGuid();
            var roleIdManager = Guid.NewGuid();

            context.Roles.AddOrUpdate(
                r => r.Name,
                new Role { Guid = roleIdVendor, Name = "vendor" },
                new Role { Guid = roleIdAdmin, Name = "admin" },
                new Role { Guid = roleIdManager, Name = "manager" }
            );

            // Contoh seed untuk Account
            var accountAdmin = new Account
            {
                Guid = Guid.NewGuid(),
                Password = HashHandler.HashPassword("Admin2023"),
                RoleGuid = roleIdAdmin,
                Status = StatusAccount.Approved,
            };

            var accountManager = new Account
            {
                Guid = Guid.NewGuid(),
                Password = HashHandler.HashPassword("Manager2023"),
                RoleGuid = roleIdManager,
                Status = StatusAccount.Approved,
            };

            context.Accounts.AddOrUpdate(
                a => a.Guid,
                accountAdmin,
                accountManager
            );

            // Contoh seed untuk Company
            var company1 = new Company
            {
                Guid = accountAdmin.Guid, // Asumsikan menggunakan Guid yang sama dengan account untuk simplicitas
                Name = "Admin Company",
                Email = "admin@company.com",
                PhoneNumber = "1234567890",
                // Asumsikan Anda memiliki kolom Foto, ganti dengan URL atau path yang benar
                Foto = "path/to/admin/company/photo"
            };

            var company2 = new Company
            {
                Guid = accountManager.Guid, // Asumsikan menggunakan Guid yang sama dengan account untuk simplicitas
                Name = "Manager Logistic Company",
                Email = "manager@logisticcompany.com",
                PhoneNumber = "0987654321",
                // Asumsikan Anda memiliki kolom Foto, ganti dengan URL atau path yang benar
                Foto = "path/to/manager/logistic/company/photo"
            };

            context.Companies.AddOrUpdate(
                c => c.Guid,
                company1,
                company2
            );

            // Save all changes to the database
            context.SaveChanges();
        }
    }
}
*/