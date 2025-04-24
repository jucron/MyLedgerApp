using MyLedgerApp.Domain;

namespace MyLedgerApp.Repositories
{
    public interface ITransactionRepository
    {
        IEnumerable<Transaction> GetAllTransactions();
        Transaction? GetTransactionById(Guid id);
    }
}
