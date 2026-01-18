namespace MyLedgerApp.Domain.Entities
{
    public class Employee: User
    {
        public string ServiceCenter { get; set; } = null!;
        public ICollection<Ledger> Ledgers { get; set; } = [];
    }
}