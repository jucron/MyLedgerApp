using MyLedgerApp.Domain.Entities;

namespace MyLedgerApp.Infrastructure.Repositories
{
    public interface ITransactionRepository
    {
        /*todo
         * var transaction = await _db.Transactions
            .Include(t => t.Ledger)
                .ThenInclude(l => l.Client)
            .FirstOrDefaultAsync(t => t.Id == id);
        */
        Task<IEnumerable<Transaction>> GetTransactionsByClientId(Guid clientId, CancellationToken ct);
        Task<Transaction?> GetTransactionById(Guid id, CancellationToken ct);
        Task DeleteTransaction(Transaction transaction, CancellationToken ct);
        Task AddTransaction(Transaction transaction, CancellationToken ct);
    }
}
