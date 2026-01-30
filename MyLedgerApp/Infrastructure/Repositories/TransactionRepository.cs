using Microsoft.EntityFrameworkCore;
using MyLedgerApp.Domain.Entities;
using MyLedgerApp.Infrastructure.DbConfig;
using MyLedgerApp.Utils;

namespace MyLedgerApp.Infrastructure.Repositories
{
    public class TransactionRepository(AppDbContext db) : ITransactionRepository
    {
        private readonly AppDbContext _db = db;
        public async Task AddTransaction(Transaction transaction)
        {
            await _db.AddAsync(transaction, CTokenHolder.Current);
        }

        public void DeleteTransaction(Transaction transaction)
        {
            _db.Remove(transaction);
        }

        public async Task<Transaction?> GetTransactionById(Guid id, bool isTracking = false)
        {
            IQueryable<Transaction> query = isTracking ? _db.Transactions : _db.Transactions.AsNoTracking();

            query = query
                .Include(t => t.Ledger)
                .ThenInclude(t => t.Client);
            
            return await query.FirstOrDefaultAsync(t => t.Id == id, CTokenHolder.Current);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByClientId(Guid clientId, bool isTracking = false)
        {
            IQueryable<Transaction> query = isTracking ? _db.Transactions : _db.Transactions.AsNoTracking();

            query = query
                .Where(t => TryUtils.AllNotNull(t.Ledger, t.Ledger.Client) && t.Ledger.Client.Id == clientId)
                .Include(t => t.Ledger)
                .ThenInclude(t => t.Client);

            return await query.ToListAsync(CTokenHolder.Current);
        }
    }
}
