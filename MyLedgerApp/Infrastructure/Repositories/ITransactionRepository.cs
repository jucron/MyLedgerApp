using MyLedgerApp.Domain.Entities;

namespace MyLedgerApp.Infrastructure.Repositories
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetTransactionsByClientId(Guid clientId, bool isTracking = false);
        Task<Transaction?> GetTransactionById(Guid id, bool isTracking = false);
        void DeleteTransaction(Transaction transaction);
        Task AddTransaction(Transaction transaction);
    }
}
