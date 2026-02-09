namespace MyLedgerApp.Api.v1.Models
{
    public class ChangePasswordRequest
    {
        public string RecoveryToken { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
    }
}
