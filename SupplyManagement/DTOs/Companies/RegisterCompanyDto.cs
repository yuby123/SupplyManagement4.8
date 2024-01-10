using SupplyManagement.Models;
using SupplyManagement.Utilities.Enums;
using System;
using System.Web;

namespace SupplyManagement.DTOs.Companies
{
    public class RegisterCompanyDto
    {
        public string NameCompany { get; set; }
        public HttpPostedFileBase FotoCompany { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AddressCompany { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

       
    }
}
