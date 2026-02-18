using MyLedgerApp.Api.v1.Models;

namespace MyLedgerApp.Application.Services.Transactions
{
    public interface ITransactionService
    {
        Task<TransactionDTO> AddTransaction(TransactionRequest transactionDTO);
        Task DeleteTransaction(Guid id);
        Task<TransactionDTO> GetTransactionById(Guid id);
        Task<IEnumerable<TransactionDTO>> GetTransactionsByClient(Guid clientId);
    }
}
