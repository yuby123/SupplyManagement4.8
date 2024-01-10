using SupplyManagement.Contracts;
using SupplyManagement.Models;


namespace SupplyManagement.Contracts
{
    public interface IAccountRepository : IGeneralRepository<Account>
    {
        Account GetByCompanyEmail(string companyEmail);
    }
}
