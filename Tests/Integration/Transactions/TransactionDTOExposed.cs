using MyLedgerApp.Api.v1.Models;

namespace Tests.Integration.Transactions
{
    public class TransactionDTOExposed: TransactionDTO
    {
        public new string Type { get; set; } = null!;
    }
}
