using MyLedgerApp.Domain.Entities;

namespace MyLedgerApp.Infrastructure.Repositories
{
    public interface ILedgerRepository
    {
        Task<IEnumerable<Ledger>> GetAllLedgers(bool includeTransactions);
        Task<Ledger?> GetLedgerById(Guid id, bool includeTransactions, bool isTracking = false);
        void DeleteLedger(Ledger ledger);
        Task AddLedger(Ledger ledger);
    }
}
