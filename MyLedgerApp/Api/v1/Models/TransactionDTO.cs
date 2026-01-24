using MyLedgerApp.Domain.Entities;

namespace MyLedgerApp.Api.v1.Models
{
    public class TransactionDTO
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public string Description { get; set; } = null!;
        public string ClientName { get; set; } = null!;

    }
}
