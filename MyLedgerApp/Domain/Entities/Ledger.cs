namespace MyLedgerApp.Domain.Entities
{
    public class Ledger
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal CurrentBalance = 0;
        public List<Transaction> Transactions { get; set; } = [];
        public Client Client { get; set; }
        public Employee Employee { get; set; }

    }
}