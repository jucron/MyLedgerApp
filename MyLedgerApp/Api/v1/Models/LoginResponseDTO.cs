namespace MyLedgerApp.Api.v1.Models
{
    public class LoginResponseDTO
    {
        public string Username { get; internal set; } = null!;
        public string Token { get; internal set; } = null!;
    }
}
