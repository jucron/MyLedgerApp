using MyLedgerApp.Domain.Entities;

namespace MyLedgerApp.Infrastructure.Repositories
{
    public interface ILedgerRepository
    {
        Task<IEnumerable<Ledger>> GetAllLedgers(bool includeTransactions, CancellationToken ct);
        Task<Ledger?> GetLedgerById(Guid id, bool includeTransactions, CancellationToken ct);
        Task DeleteLedger(Ledger ledger, CancellationToken ct);
        Task AddLedger(Ledger ledger, CancellationToken ct);
    }
}
