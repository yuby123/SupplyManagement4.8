
using SupplyManagement.Models;
using System;

namespace SupplyManagement.DTOs.Companies
{

    public class CompanyDetailDto
    {
        public string NameCompany { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string StatusAccount { get; set; }
        public string RoleName { get; set; }

    }
}
