using SupplyManagement.Contracts;
using SupplyManagement.Data;
using SupplyManagement.Models;
using SupplyManagement.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupplyManagement.Repositories
{
    public class VendorRepository : GeneralRepository<Vendor>, IVendorRepository
    {
        public VendorRepository(SMDbContext context) : base(context) { }
        public IEnumerable<Vendor> GetVendorsByCompany(Guid companyGuid)
        {
            return _context.Vendors.Where(v => v.CompanyGuid == companyGuid).ToList();
        }
/*        public IEnumerable<Vendor> GetVendorByAccountStatus(StatusVendor vendorStatus)
        {
            
        }*/

    }
}
