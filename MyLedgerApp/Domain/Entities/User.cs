namespace MyLedgerApp.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public Credential Credential { get; set; } = null!;
        public DateTime dateCreated = DateTime.UtcNow;

    }
}
