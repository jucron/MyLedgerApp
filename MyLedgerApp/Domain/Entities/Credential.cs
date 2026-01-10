namespace MyLedgerApp.Domain.Entities
{
    public class Credential
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }

        public bool VerifyPassword(string password) =>
            !string.IsNullOrEmpty(password) && BCrypt.Net.BCrypt.Verify(password, PasswordHash);
    }
}