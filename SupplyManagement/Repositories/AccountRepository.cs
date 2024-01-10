using SupplyManagement.Models;
using SupplyManagement.Contracts;
using SupplyManagement.Data;
using System.Linq;

namespace SupplyManagement.Repositories
{
    public class AccountRepository : GeneralRepository<Account>, IAccountRepository
    {
        private readonly SMDbContext _context;

        public AccountRepository(SMDbContext context) : base(context)
        {
            _context = context;
        }

        public Account GetByCompanyEmail(string companyEmail)
        {
            var account = _context.Accounts
                .Join(
                    _context.Companies,
                    acc => acc.Guid,  // Use a distinct name for the account parameter
                    company => company.Guid,
                    (acc, company) => new  // Use a distinct name for the anonymous type property
                    {
                        Account = acc,
                        Company = company
                    }
                )
                .Where(joinResult => joinResult.Company.Email == companyEmail)
                .Select(joinResult => joinResult.Account)
                .FirstOrDefault();

            return account;
        }
    }
}
