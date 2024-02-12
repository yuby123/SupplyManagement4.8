using SupplyManagement.Models;
using SupplyManagement.Utilities.Enums;
using System.Collections.Generic;

namespace SupplyManagement.Contracts
{
    public interface ICompanyRepository : IGeneralRepository<Company>
    {
        Company GetByCompanyEmail(string companyEmail);
        Company GetAdminEmployee();
        IEnumerable<Company> GetCompaniesByAccountStatus(StatusAccount accountStatus);
    }
}
