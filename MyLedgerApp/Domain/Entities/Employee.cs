namespace MyLedgerApp.Domain.Entities
{
    public class Employee: User
    {
        public required string serviceCenter;
        public List<Ledger> Ledgers { get; set; } = [];
    }
}