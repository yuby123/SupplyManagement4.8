using SupplyManagement.Models;

namespace SupplyManagement.Contracts
{
    public interface ICompanyRepository : IGeneralRepository<Company>
    {
        Company GetByCompanyEmail(string companyEmail);
        Company GetAdminEmployee();
    }
}
