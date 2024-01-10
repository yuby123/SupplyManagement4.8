using SupplyManagement.Contracts;
using SupplyManagement.Data;
using SupplyManagement.Models;
using System;
using System.Linq;

namespace SupplyManagement.Repositories
{
    public class RoleRepository : GeneralRepository<Role>, IRoleRepository
    {
        public RoleRepository(SMDbContext context) : base(context) { }

        // Correct the return type to Nullable<Guid>
        public Nullable<Guid> GetDefaultGuid()
        {
            // Mengambil role dengan nama "vendor" dari database
            return _context.Set<Role>().FirstOrDefault(r => r.Name == "vendor")?.Guid;
        }
        public string GetRoleNameByGuid(Guid roleGuid)
        {
            var role = _context.Roles.SingleOrDefault(r => r.Guid == roleGuid);

            return role?.Name;
        }
    }
}
