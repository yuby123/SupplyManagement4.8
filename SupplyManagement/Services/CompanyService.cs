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
                    string fileExtension = Path.GetExtension(fotoCompany.FileName);
                    photoFileName = $"{Guid.NewGuid():N}_{DateTime.Now:yyyyMMddHHmmss}{fileExtension}";

                    string fullFilePath = Path.Combine(basePath, photoFileName);
                    fotoCompany.SaveAs(fullFilePath);


                }
                Guid companyGuid = Guid.NewGuid();
                Company newCompany = new Company
                {
                    Guid = companyGuid,
                    Name = registerDto.NameCompany,
                    Address = registerDto.AddressCompany,
                    Email = registerDto.Email,
                    PhoneNumber = registerDto.PhoneNumber,
                    Foto = photoFileName
                };

                _companyRepository.Create(newCompany);

                Account newAccount = new Account
                {
                    Guid = newCompany.Guid,
                    RoleGuid = _roleRepository.GetDefaultGuid() ?? throw new Exception("Default role not found"),
                    Password = HashHandler.HashPassword(registerDto.Password),
                    Status = StatusAccount.Requested
                };

                _accountRepository.Create(newAccount);
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
