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
        private readonly IUserRepository _userRepository;
        private readonly JWTHelper _jwtHelper;

        public AuthService(IConfiguration configuration, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _jwtHelper = new JWTHelper(configuration);
        }

        public LoginResponseDTO Authenticate(LoginRequest request)
        {
            // Validate credentials 
            var userFromRepo = _userRepository.GetUserByUsername(request.Username);
            ValidateUserCredentials(request.Password, userFromRepo);

            var token = _jwtHelper.GenerateToken(request.Username);

            return new LoginResponseDTO
            {
                Username = request.Username,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        private static void ValidateUserCredentials(string password, User? user)
        {
            if (user == null || !user.Credential.VerifyPassword(password))
                throw new UnauthorizedAccessException("Invalid username or password.");

        }

        public LoginResponseDTO RefreshToken(string token)
        {
            var remaining = _jwtHelper.GetTimeRemaining(token);
            var tokenExpMinutesLimit = _jwtHelper.TokenExpireMinutes/3;

            if (remaining > TimeSpan.FromMinutes(tokenExpMinutesLimit))
               throw new ArgumentException("Token is still valid.");

            var username = _jwtHelper.GetClaim(token, ClaimTypes.Name) ?? throw new UnauthorizedAccessException("Invalid token.");

            var refreshedToken = _jwtHelper.GenerateToken(username);

            return new LoginResponseDTO
            {
                Username = username,
                Token = new JwtSecurityTokenHandler().WriteToken(refreshedToken)
            };


        }
    }
}
