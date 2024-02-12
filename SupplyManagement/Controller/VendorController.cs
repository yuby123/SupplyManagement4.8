using SupplyManagement.Contracts;
using SupplyManagement.DTOs.Accounts;
using SupplyManagement.Models;
using SupplyManagement.Services;
using SupplyManagement.Utilities.Enums;
using System;
using System.Web.Mvc;

namespace SupplyManagement.Controllers
{
    public class VendorController : Controller
    {
        private readonly AccountService _accountService;
        private readonly IAccountRepository _accountRepository;
        private readonly IVendorRepository _vendorRepository;

        public VendorController(AccountService accountService, IAccountRepository accountRepository, IVendorRepository vendorRepository)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _accountRepository = accountRepository;
            _vendorRepository = vendorRepository;
        }

        public ActionResult Create()
        {
            var userRole = Session["UserRole"] as string;
            if (userRole == "company")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Unauthorized", "Error");
            }
            
        }

        public ActionResult VendorList()
        {
            var userRole = Session["UserRole"] as string;
            var userIdString = Session["userId"] as string;

            if (userRole == "company" && Guid.TryParse(userIdString, out Guid companyGuid))
            {
                var vendors = _vendorRepository.GetVendorsByCompany(companyGuid); 
                return View(vendors);
            }
            else
            {
                return RedirectToAction("Unauthorized", "Error");
            }
        }


        [HttpPost]
        public ActionResult Create(Vendor model)
        {
            if (ModelState.IsValid)
            {
                var userIdString = Session["userId"] as string;

                if (Guid.TryParse(userIdString, out Guid userId))
                {
                    Guid vendorId = Guid.NewGuid();
                    _vendorRepository.Create(new Vendor
                    {
                        Guid = vendorId,
                        BidangUsaha = model.BidangUsaha,
                        JenisPerusahaan = model.JenisPerusahaan,
                        CompanyGuid = userId,
                        StatusVendor = StatusVendor.requested
                    });

                    TempData["SuccessMessage"] = "Create Vendor berhasil!";

                    /*return RedirectToAction("Login", "Account");*/
                    return RedirectToAction("VendorList");
                }

                else
                {
                    TempData["ErrorMessage"] = "Format ID pengguna tidak valid.";
                    return RedirectToAction("Error", "Shared");
                }
            }
            return View(model);
        }

        /*public ActionResult VendorRequested()
        {
            var userRole = Session["UserRole"] as string;
            if (userRole == "admin")
            {
                var requestedVendor = _vendorRepository.GetCompaniesByAccountStatus(StatusVendor.requested);
                return View(requestedVendor);
            }
            else
            {
                return RedirectToAction("Unauthorized", "Error");
            }

        }*/

    }
}
