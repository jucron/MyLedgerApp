namespace MyLedgerApp.Domain
{
    public class Ledger
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public List<Transaction> Transactions { get; set; } = [];
        public decimal CurrentBalance = 0;

    }
}