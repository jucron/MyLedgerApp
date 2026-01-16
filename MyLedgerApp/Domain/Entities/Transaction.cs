namespace MyLedgerApp.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public string Description { get; set; } = "";

        public Guid LedgerId { get; set; }
        public Ledger Ledger { get; set; }

    }
}
