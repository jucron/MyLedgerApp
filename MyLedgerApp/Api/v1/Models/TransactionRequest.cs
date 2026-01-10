using MyLedgerApp.Domain.Entities;

namespace MyLedgerApp.Api.v1.Models
{
    public class TransactionRequest
    {
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public string Description { get; set; }
        public Guid LedgerId { get; set; }
    }
}
