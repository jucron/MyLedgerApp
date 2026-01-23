namespace MyLedgerApp.Domain.Entities.Users
{
    public class Credential
    {
        public string Username { get; set; } = null!;
        private string PasswordHash { get; set; } = null!;

        public string StorePassword
        {
            // Only allow setting
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Password cannot be empty.", nameof(value));

                PasswordHash = BCrypt.Net.BCrypt.HashPassword(value);
            }
        }

        public bool VerifyPassword(string password) =>
            !string.IsNullOrEmpty(password) && BCrypt.Net.BCrypt.Verify(password, PasswordHash);
    }
}