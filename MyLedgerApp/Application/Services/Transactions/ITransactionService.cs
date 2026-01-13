using MyLedgerApp.Api.v1.Models;

namespace MyLedgerApp.Application.Services.Transactions
{
    public interface ITransactionService
    {
        TransactionDTO AddTransaction(TransactionRequest transactionDTO);
        void DeleteTransaction(Guid id);
        TransactionDTO GetTransactionById(Guid id);
        IEnumerable<TransactionDTO> GetTransactions(Guid clientId);
    }
}
