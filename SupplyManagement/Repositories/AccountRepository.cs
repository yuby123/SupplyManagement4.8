using SupplyManagement.Models;
using SupplyManagement.Contracts;
using SupplyManagement.Data;
using System.Linq;
using System;
using System.Data.Entity;

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
                    acc => acc.Guid,
                    company => company.Guid,
                    (acc, company) => new
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
        public Account GetAccountByGuid(Guid accountGuid)
        {
            return _context.Accounts.SingleOrDefault(a => a.Guid == accountGuid);
        }

        public void UpdateAccountStatus(Account account)
        {
            _context.Entry(account).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
