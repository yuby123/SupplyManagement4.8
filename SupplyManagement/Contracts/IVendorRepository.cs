using SupplyManagement.Models;
using SupplyManagement.Utilities.Enums;
using System;
using System.Collections.Generic;

namespace SupplyManagement.Contracts
{
    public interface IVendorRepository : IGeneralRepository<Vendor>
    {
        IEnumerable<Vendor> GetVendorsByCompany(Guid companyGuid);
/*        IEnumerable<Vendor> GetVendorByAccountStatus(StatusVendor vendorStatus);*/
    }
}
