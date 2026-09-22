using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuickKart.Application.Interfaces.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace QuickKart.Application.Services
{
    public class JwtService : IJwtService
    {
        public readonly IConfiguration _Configuration;
        public JwtService(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public string GenerateToken(Guid id, string email, string role)
        {
            var jwtSettings = _Configuration.GetSection("JwtSettings");
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, id.ToString()),
                new(JwtRegisteredClaimNames.Email, email),
                new(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["Key"]!));

            var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    Convert.ToDouble(jwtSettings["ExpiryMinutes"])
                ),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
