namespace MyLedgerApp.Domain.Entities
{
    public class Client: User
    {
        public List<Ledger> Ledgers { get; set; } = [];
    }
}