namespace MyLedgerApp.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Email { get; set; }
        public Credential Credential { get; set; }
        public DateTime dateCreated = DateTime.UtcNow;

    }
}
