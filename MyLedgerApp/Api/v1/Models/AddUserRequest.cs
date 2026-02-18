namespace MyLedgerApp.Api.v1.Models
{
    public class AddUserRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
    }
}
