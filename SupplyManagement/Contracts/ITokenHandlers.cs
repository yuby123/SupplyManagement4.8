using SupplyManagement.DTOs.Tokens;
using System.Collections.Generic;
using System.Security.Claims;

namespace SupplyManagement.Contracts
{
    public interface ITokenHandlers
    {
        string Generate(IEnumerable<Claim> claims);

        ClaimsDto ExtractClaimsFromJwt(string token);
    }
}
