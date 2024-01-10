using SupplyManagement.DTOs.Companies;
using SupplyManagement.Services;
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


        public CompanyController(CompanyService companyService)
        {
            _companyService = companyService;

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
                // Mendapatkan path dasar untuk menyimpan foto
                string basePath = Server.MapPath("~/Utilities/File/FotoCompany/");

                if (_companyService.Register(registerDto, fotoCompany, basePath))
                {
                    // Menetapkan pesan sukses ke TempData untuk ditampilkan di view tujuan
                    TempData["SuccessMessage"] = "Registrasi berhasil!";

                    // Redirect ke halaman Index atau halaman lain setelah registrasi berhasil
                    return RedirectToAction("Index", "Company");
                }
                else
                {
                    ModelState.AddModelError("", "Registration failed. Please try again.");
                }
            }

            // Jika model tidak valid atau registrasi gagal, tampilkan form lagi dengan pesan kesalahan
            return View(registerDto);
        }
    }
}