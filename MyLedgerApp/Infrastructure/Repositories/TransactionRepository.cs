using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyLedgerApp.Domain.Entities;
using MyLedgerApp.Infrastructure.DbConfig;

namespace MyLedgerApp.Infrastructure.Repositories
{
    public class TransactionRepository(AppDbContext db) : ITransactionRepository
    {
        private readonly AppDbContext _db = db;
        public async Task AddTransaction(Transaction transaction, CancellationToken ct)
        {
            await _db.AddAsync(transaction, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteTransaction(Transaction transaction, CancellationToken ct)
        {
            _db.Remove(transaction);
            await _db.SaveChangesAsync(ct);
        }

        public async Task<Transaction?> GetTransactionById(Guid id, CancellationToken ct)
        {
            IQueryable<Transaction> query = _db.Transactions
                .AsNoTracking()
                .Include(t => t.Ledger)
                .ThenInclude(t => t.Client);
            
            return await query.FirstOrDefaultAsync(t => t.Id == id, ct);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByClientId(Guid clientId, CancellationToken ct)
        {
            IQueryable<Transaction> query = _db.Transactions
                .AsNoTracking()
                .Where(t => t.Ledger.Client.Id == clientId)
                .Include(t => t.Ledger)
                .ThenInclude(t => t.Client);

            return await query.ToListAsync(ct);
        }
    }
}
