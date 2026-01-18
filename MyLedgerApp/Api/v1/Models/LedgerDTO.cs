namespace MyLedgerApp.Api.v1.Models
{
    public class LedgerDTO
    {
        public Guid Id { get; set; }
        public decimal CurrentBalance { get; set; }
        public Guid ClientId { get; set; }
        public Guid EmployeeId { get; set; }

        public List<TransactionDTO>? Transactions { get; set; }

    }
}