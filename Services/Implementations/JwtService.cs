using Ecommerce.Models;
using Ecommerce.Options;
using Ecommerce.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Ecommerce.Services.Implementations
{
    public class JwtService: IJwtService
    {
        private readonly JwtSetting _jwtSetting;

        public JwtService(IOptions<JwtSetting> jwtOptions)
        {
            _jwtSetting = jwtOptions.Value;
        }

        public string GenerateToken(User user)
        {
            var roles = user.UserRoles
                   .Select(ur => ur.Role.Name)
                   .FirstOrDefault() ?? string.Empty;
            var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.FullName),
            new(ClaimTypes.Role, roles),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

            //foreach (var roleName in roles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, roleName));
            //}

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSetting.Key));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSetting.Issuer,
                audience: _jwtSetting.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSetting.ExpireMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
    }
}
