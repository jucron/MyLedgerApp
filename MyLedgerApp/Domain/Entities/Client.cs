namespace MyLedgerApp.Domain.Entities
{
    public class Client: User
    {
        public ICollection<Ledger> Ledgers { get; set; } = [];
    }
}