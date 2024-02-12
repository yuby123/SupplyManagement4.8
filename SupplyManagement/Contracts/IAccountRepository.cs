using SupplyManagement.Contracts;
using SupplyManagement.Models;
using System;

namespace SupplyManagement.Contracts
{
    public interface IAccountRepository : IGeneralRepository<Account>
    {
        Account GetByCompanyEmail(string companyEmail);
        Account GetAccountByGuid(Guid accountGuid);
        void UpdateAccountStatus(Account account);
    }
}
