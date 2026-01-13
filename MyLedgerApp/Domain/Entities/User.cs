namespace MyLedgerApp.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required Credential Credential { get; set; }
        public DateTime dateCreated = DateTime.UtcNow;

    }
}
