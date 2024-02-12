namespace SupplyManagement.Migrations
{
    using SupplyManagement.Data;
    using SupplyManagement.Models;
    using SupplyManagement.Utilities.Enums;
    using SupplyManagement.Utilities.Handler;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SupplyManagement.Data.SMDbContext>
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
                new Role { Guid = roleIdVendor, Name = "company" },
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
                Guid = accountAdmin.Guid,
                Name = "Admin Company",
                Email = "admin@company.com",
                Address = "null", // Use null for absence of a value
                PhoneNumber = "12345678", // Use null for absence of a value
                Foto = "null", // Use null for absence of a value
            };

            var company2 = new Company
            {
                Guid = accountManager.Guid,
                Name = "Manager Logistic Company",
                Email = "manager@logisticcompany.com",
                Address = "null", // Use null for absence of a value
                PhoneNumber = "87654321", // Use null for absence of a value
                Foto = "null", // Use null for absence of a value

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
