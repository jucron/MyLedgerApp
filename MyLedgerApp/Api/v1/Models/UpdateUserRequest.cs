namespace MyLedgerApp.Api.v1.Models
{
    public class UpdateUserRequest
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
    }
}
