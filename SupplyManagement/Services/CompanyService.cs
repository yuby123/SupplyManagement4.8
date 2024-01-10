using SupplyManagement.DTOs.Accounts;
using System;
using System.IO;
using SupplyManagement.Contracts;
using SupplyManagement.Models;
using SupplyManagement.Utilities.Handler;
using System.Linq;
using SupplyManagement.DTOs.Companies;
using System.Transactions;
using SupplyManagement.Data;
using SupplyManagement.Utilities.Enums;
using System.Web;


namespace SupplyManagement.Services
{
    public class CompanyService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly SMDbContext _sMDbContext;

        public CompanyService(IAccountRepository accountRepository, ICompanyRepository companyRepository, IRoleRepository roleRepository, SMDbContext sMDbContext)
        {
            _accountRepository = accountRepository;
            _companyRepository = companyRepository;
            _roleRepository = roleRepository;
            _sMDbContext = sMDbContext;
        }

        public bool Register(RegisterCompanyDto registerDto, HttpPostedFileBase fotoCompany, string basePath)
        {
            var transaction = _sMDbContext.Database.BeginTransaction();
            try
            {
                string photoFileName = null;
                if (fotoCompany != null && fotoCompany.ContentLength > 0)
                {
                    // Generate a unique file name
                    string fileExtension = Path.GetExtension(fotoCompany.FileName);
                    photoFileName = $"{Guid.NewGuid():N}_{DateTime.Now:yyyyMMddHHmmss}{fileExtension}";

                    // Combine the base path with the file name to create a full path
                    string fullFilePath = Path.Combine(basePath, photoFileName);

                    // Save the file to the server
                    fotoCompany.SaveAs(fullFilePath);


                }
                Guid companyGuid = Guid.NewGuid();
                // Create a new Company object
                Company newCompany = new Company
                {
                    Guid = companyGuid,
                    Name = registerDto.NameCompany,
                    Address = registerDto.AddressCompany,
                    Email = registerDto.Email,
                    PhoneNumber = registerDto.PhoneNumber,
                    Foto = photoFileName
                };

                // Save the new Company
                _companyRepository.Create(newCompany);

                // Create a new Account object
                Account newAccount = new Account
                {
                    // Set properties, including the link to the new Company
                    Guid = newCompany.Guid,
                    RoleGuid = _roleRepository.GetDefaultGuid() ?? throw new Exception("Default role not found"),
                    Password = HashHandler.HashPassword(registerDto.Password),
                    Status = StatusAccount.Requested
                };

                // Save the new Account
                _accountRepository.Create(newAccount);

                // Save changes to the database
                _sMDbContext.SaveChanges();

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during registration: {ex.Message}");
                transaction.Rollback();
                return false;
            }
        }


    }
}
