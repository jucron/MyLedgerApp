using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Application.Properties;
using MyLedgerApp.Domain.Entities.Users;
using MyLedgerApp.Infrastructure.Repositories;
using static MyLedgerApp.Utils.Exceptions;

namespace MyLedgerApp.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JWTHelper _jwtHelper;

        public AuthService(IAppProperties prop, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _jwtHelper = new JWTHelper(prop.JwtSettings);
        }

        public async Task<LoginResponseDTO> Authenticate(LoginRequest request)
        {
            var errorMsg = "Invalid username or password";

            var userFromRepo = await _userRepository.GetUserByUsername(request.Username) ??
                throw new UnauthorizedAccessException(errorMsg);

            if (userFromRepo.Credential.VerifyPassword(request.Password))
                throw new UnauthorizedAccessException(errorMsg);

            var token = _jwtHelper.GenerateToken(request.Username);

            return new LoginResponseDTO
            {
                Username = request.Username,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        public LoginResponseDTO RefreshToken(string token)
        {
            var remaining = _jwtHelper.GetTimeRemaining(token);

            if (remaining > TimeSpan.FromMinutes(5))
               throw new ArgumentException("Token is still valid. Please refresh when token expires in 5 minutes or less");

            var username = _jwtHelper.GetClaim(token, ClaimTypes.Name) ?? throw new UnauthorizedAccessException("Invalid token");

            var refreshedToken = _jwtHelper.GenerateToken(username);

            return new LoginResponseDTO
            {
                Username = username,
                Token = new JwtSecurityTokenHandler().WriteToken(refreshedToken)
            };


        }
    }
}
