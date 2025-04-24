using MyLedgerApp.Domain;

namespace MyLedgerApp.Api.v1.Models
{
    public class TransactionDTO
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public string Description { get; set; }
        public string ClientName { get; set; }
        public string EmployeeName { get; set; }

    }
}
