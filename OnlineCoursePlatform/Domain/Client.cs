namespace MyLedgerApp.Domain
{
    public class Client: User
    {
        public List<Ledger> Ledgers { get; set; } = [];
    }
}