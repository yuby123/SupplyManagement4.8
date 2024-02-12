using SupplyManagement.Contracts;
using SupplyManagement.DTOs.Companies;
using SupplyManagement.Services;
using SupplyManagement.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SupplyManagement.Controllers
{
    public class CompanyController : Controller
    {
        private readonly CompanyService _companyService;
        private readonly IAccountRepository _accountRepository;
        private readonly ICompanyRepository _companyRepository;


        public CompanyController(CompanyService companyService, IAccountRepository accountRepository, ICompanyRepository companyRepository )
        {
            _companyService = companyService;
            _accountRepository = accountRepository;
            _companyRepository = companyRepository;

        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterCompanyDto registerDto, HttpPostedFileBase fotoCompany)
        {
            if (ModelState.IsValid)
            {
                string basePath = Server.MapPath("~/Utilities/File/FotoCompany/");

                if (_companyService.Register(registerDto, fotoCompany, basePath))
                {
                    

                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Registration failed. Please try again.");
                }
            }

            return View(registerDto);
        }

        public ActionResult CompanyRequested()
        {
            var userRole = Session["UserRole"] as string;
            if (userRole == "admin")
            {
                var requestedCompanies = _companyRepository.GetCompaniesByAccountStatus(StatusAccount.Requested);
                return View(requestedCompanies);
            }
            else
            {
                return RedirectToAction("Unauthorized", "Error");
            }

        }
    }
}