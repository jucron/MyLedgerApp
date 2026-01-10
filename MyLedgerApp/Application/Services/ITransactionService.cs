using MyLedgerApp.Api.v1.Models;

namespace MyLedgerApp.Application.Services
{
    public interface ITransactionService
    {
        TransactionDTO AddTransaction(TransactionRequest transactionDTO);
        void DeleteTransaction(Guid id);
        TransactionDTO GetTransactionById(Guid id);
        IEnumerable<TransactionDTO> GetAllTransactions();
    }
}
