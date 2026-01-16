namespace MyLedgerApp.Domain.Entities
{
    public class Ledger
    {
        public Guid Id { get; set; }
        public decimal CurrentBalance = 0;
        public List<Transaction> Transactions { get; set; } = [];
        public bool IsClosed { get; set; }

        public Guid ClientId { get; set; }
        public Client Client { get; set; }

        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }

    }
}