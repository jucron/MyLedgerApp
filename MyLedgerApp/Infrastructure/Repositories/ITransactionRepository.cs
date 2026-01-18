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
        IEnumerable<Transaction> GetTransactionsByClientId(Guid clientId);
        Transaction? GetTransactionById(Guid id);
        bool DeleteTransaction(Transaction transaction);
        void AddTransaction(Transaction transaction);
    }
}
