using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Domain.Entities;
using MyLedgerApp.Infrastructure.Repositories;

namespace MyLedgerApp.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AuthService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public LoginResponseDTO Authenticate(LoginRequest request)
        {
            // Validate credentials 
            var userFromRepo = _userRepository.GetUserByUsername(request.Username);
            ValidateUserCredentials(request.Password, userFromRepo);

            var token = GenerateToken(request.Username);

            return new LoginResponseDTO
            {
                Username = request.Username,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        private SecurityToken GenerateToken(string username)
        {
            var keyString = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured.");
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            return new JwtSecurityToken(
                expires: DateTime.UtcNow.AddMinutes(30),
                claims: new[] { new Claim(ClaimTypes.Name, username) },
                signingCredentials: creds
            );

        }

        private static void ValidateUserCredentials(string password, User? user)
        {
            if (user == null || !user.Credential.VerifyPassword(password))
                throw new UnauthorizedAccessException("Invalid username or password.");

        }
    }
}
