using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Domain;

namespace MyLedgerApp.Services
{
    public interface ITransactionService
    {
        TransactionDTO AddTransaction(Transaction transaction);
        TransactionDTO GetTransactionById(Guid id);
        IEnumerable<TransactionDTO> GetTransactions();
    }
}
