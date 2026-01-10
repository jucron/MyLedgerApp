using MyLedgerApp.Api.v1.Models;

namespace MyLedgerApp.Application.Services.Auth
{
    public interface IAuthService
    {
        LoginResponseDTO Authenticate(LoginRequest request);
    }
}
