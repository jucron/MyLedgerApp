using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MyLedgerApp.Application.Services.Auth
{
    public class JWTHelper
    {
        public string TokenKey { get; private set; }
        public int TokenExpireMinutes { get; private set; }

        public JWTHelper(IConfiguration configuration)
        {
            TokenKey = configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured.");
            TokenExpireMinutes = int.TryParse(configuration["Jwt:ExpireMinutes"], out var expireMinutesValue) ? expireMinutesValue : 30;
        }

        public SecurityToken GenerateToken(string username)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                expires: DateTime.UtcNow.AddMinutes(TokenExpireMinutes),
                claims: new[] { new Claim(ClaimTypes.Name, username) },
                signingCredentials: creds
            );

        }
        public string? GetClaim(string token, string claimType)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            return jwt.Claims
                      .FirstOrDefault(c => c.Type == claimType)
                      ?.Value;
        }

        public TimeSpan GetTimeRemaining(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            return jwt.ValidTo - DateTime.UtcNow;
        }
    }
}
