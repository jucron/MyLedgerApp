using MyLedgerApp.Api.v1.Models;

namespace MyLedgerApp.Application.Services.Transactions
{
    public interface ITransactionService
    {
        Task<TransactionDTO> AddTransaction(TransactionRequest transactionDTO, CancellationToken ct);
        Task DeleteTransaction(Guid id, CancellationToken ct);
        Task<TransactionDTO> GetTransactionById(Guid id, CancellationToken ct);
        Task<IEnumerable<TransactionDTO>> GetTransactions(Guid clientId, CancellationToken ct);
    }
}
