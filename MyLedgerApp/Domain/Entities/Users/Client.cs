namespace MyLedgerApp.Domain.Entities.Users
{
    public class Client: User
    {
        public ICollection<Ledger> Ledgers { get; set; } = [];
    }
}