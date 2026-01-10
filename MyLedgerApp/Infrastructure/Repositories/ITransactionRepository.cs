using MyLedgerApp.Domain.Entities;

namespace MyLedgerApp.Infrastructure.Repositories
{
    public interface ITransactionRepository
    {
        IEnumerable<Transaction> GetAllTransactions();
        Transaction? GetTransactionById(Guid id);
        bool DeleteTransaction(Transaction transaction);
        void AddTransaction(Transaction transaction);
    }
}
