using MyLedgerApp.Api.v1.Models;

namespace MyLedgerApp.Application.Services.Auth
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> Authenticate(LoginRequest request);
        LoginResponseDTO RefreshToken(string token);
    }
}
