using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SecureShield.Application.Services;
using SecureShield.Infrastructure.Data;

namespace SecureShield.Infrastructure.Identity;

/// <summary>
/// Admin authentication service — no ASP.NET Core Identity, no database.
/// Validates credentials against hardcoded values in StaticData and issues JWT tokens.
/// Change credentials in StaticData.cs for production deployment.
/// </summary>
public class AdminAuthService : IAdminAuthService
{
    private readonly IConfiguration _config;

    public AdminAuthService(IConfiguration config)
    {
        _config = config;
    }

    public Task<string?> AuthenticateAsync(string email, string password)
    {
        if (string.Equals(email, StaticData.AdminEmail, StringComparison.OrdinalIgnoreCase)
            && password == StaticData.AdminPassword)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, "admin"),
                new(ClaimTypes.Email, StaticData.AdminEmail),
                new(ClaimTypes.Name, StaticData.AdminName),
                new(ClaimTypes.Role, StaticData.AdminRole),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: creds);

            return Task.FromResult<string?>(new JwtSecurityTokenHandler().WriteToken(token));
        }

        return Task.FromResult<string?>(null);
    }
}
