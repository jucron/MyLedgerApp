namespace MyLedgerApp.Domain.Entities.Users
{
    public class Credential
    {
        public string Username { get; private set; } = null!;
        public string PasswordHash { get; private set; } = null!;

        private Credential() { } // EF Core

        public Credential(string username, string plainPassword)
        {
            Username = username;
            SetPassword(plainPassword);
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty.", nameof(password));

            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(PasswordHash))
                throw new InvalidOperationException("Password hash is not initialized.");

            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }
    }
}