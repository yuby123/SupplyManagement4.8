using SupplyManagement.Models;
using System;

namespace SupplyManagement.Contracts
{
    public interface IRoleRepository : IGeneralRepository<Role>
    {
        Guid? GetDefaultGuid();
        string GetRoleNameByGuid(Guid roleGuid);
    }
}
