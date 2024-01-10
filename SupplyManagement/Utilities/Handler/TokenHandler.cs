
using Microsoft.IdentityModel.Tokens;
using SupplyManagement.Contracts;
using SupplyManagement.DTOs.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace API.Utilities.Handler
{
    public class TokenHandler : ITokenHandlers
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;

        public TokenHandler()
        {
            _secretKey = ConfigurationManager.AppSettings["JWTService:SecretKey"];
            _issuer = ConfigurationManager.AppSettings["JWTService:Issuer"];
            _audience = ConfigurationManager.AppSettings["JWTService:Audience"];
        }

        public string Generate(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: signingCredentials);
            var encodedToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return encodedToken;
        }

        public ClaimsDto ExtractClaimsFromJwt(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return new ClaimsDto();
            }

            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidAudience = _audience,
                    ValidateIssuer = true,
                    ValidIssuer = _issuer,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey))
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);

                if (claimsPrincipal.Identity is ClaimsIdentity identity)
                {
                    var claims = new ClaimsDto
                    {
                        CompanyGuid = Guid.Parse(identity.FindFirst("CompanyGuid")?.Value ?? ""),
                        Name = identity.FindFirst("Name")?.Value,
                        Email = identity.FindFirst("Email")?.Value,
                        Foto = identity.FindFirst("Foto")?.Value,
                        StatusAccount = identity.FindFirst("StatusAccount")?.Value,
                        StatusVendor = identity.FindFirst("StatusVendor")?.Value,
                    };

                    var roles = identity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(claim => claim.Value).ToList();
                    claims.Role = roles;

                    return claims;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error extracting claims from JWT: " + ex.Message);
            }

            return new ClaimsDto();
        }
    }
}
