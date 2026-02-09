using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Application.Properties;
using MyLedgerApp.Infrastructure.DbSessions;
using MyLedgerApp.Infrastructure.Repositories;
using Shared.Contracts.Events;
using Shared.Contracts.Events.Publishable;

namespace MyLedgerApp.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICacheService _cacheService;
        private readonly IEventPublisher _eventPublisher;
        private readonly IDbSession _dbSession;

        private readonly JWTHelper _jwtHelper;
        private readonly CacheSettings _cacheSettings;

        public AuthService(IAppProperties prop, IUserRepository userRepository, ICacheService cacheService, IEventPublisher eventPublisher, IDbSession dbSession)
        {
            _userRepository = userRepository;
            _cacheService = cacheService;
            _eventPublisher = eventPublisher;
            _dbSession = dbSession;

            _jwtHelper = new JWTHelper(prop.JwtSettings);
            _cacheSettings = prop.CacheSettings;
        }

        public async Task<LoginResponseDTO> Authenticate(LoginRequest request)
        {
            var errorMsg = "Invalid username or password";

            var userFromRepo = await _userRepository.GetUserByUsername(request.Username) ??
                throw new UnauthorizedAccessException(errorMsg);

            if (!userFromRepo.Credential.VerifyPassword(request.Password))
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
                throw new ArgumentException("Token is still valid. Please refresh when resetToken expires in 5 minutes or less");

            var username = _jwtHelper.GetClaim(token, ClaimTypes.Name) ?? throw new UnauthorizedAccessException("Invalid resetToken");

            var refreshedToken = _jwtHelper.GenerateToken(username);

            return new LoginResponseDTO
            {
                Username = username,
                Token = new JwtSecurityTokenHandler().WriteToken(refreshedToken)
            };
        }

        public async Task ChangePassword(ChangePasswordRequest request)
        {
            var username = _cacheService.Get<string>(request.RecoveryToken);
            if (username is null) return;

            var userFromRepo = await _userRepository.GetUserByUsername(username, isTracking: true);
            if (userFromRepo is null) return;

            userFromRepo.Credential.SetPassword(request.NewPassword);
            await _dbSession.SaveChangesAsync();

            _cacheService.Remove(request.RecoveryToken);

            var evt = new PasswordChangedEvent()
            {
                Email = userFromRepo.Email,
                Username = username,
            };
            _ = _eventPublisher.PublishAsync(evt);
        }

        public async Task RequestPasswordReset(string username)
        {
            var userFromRepo = await _userRepository.GetUserByUsername(username);
            if (userFromRepo is null) return;

            var resetToken = Guid.NewGuid().ToString();

            _cacheService.Set(resetToken, username, _cacheSettings.TokenPassTimeout);

            var evt = new PasswordRecoverRequestedEvent()
            {
                Email = userFromRepo.Email,
                Username = username,
                RecoveryToken = resetToken,
                RecoveryTimeout = _cacheSettings.TokenPassTimeout
            };
            _ = _eventPublisher.PublishAsync(evt);
        }
    }
}
