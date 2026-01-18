using Microsoft.EntityFrameworkCore;
using MyLedgerApp.Domain.Entities;
using MyLedgerApp.Infrastructure.DbConfig;

namespace MyLedgerApp.Infrastructure.Repositories
{
    public class LedgerRepository(AppDbContext db) : ILedgerRepository
    {
        private readonly AppDbContext _db = db;

        public async Task AddLedger(Ledger ledger, CancellationToken ct)
        {
            await _db.AddAsync(ledger, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteLedger(Ledger ledger, CancellationToken ct)
        {
            _db.Remove(ledger);
            await _db.SaveChangesAsync(ct);
        }

        public async Task<IEnumerable<Ledger>> GetAllLedgers(bool includeTransactions, CancellationToken ct)
        {
            IQueryable<Ledger> query = _db.Ledgers
                .AsNoTracking()
                .Include(l => l.Client)
                .Include(l => l.Employee);

            if (includeTransactions)
                query = query.Include(l => l.Transactions);

            return await query.ToListAsync(ct);
        }

        public async Task<Ledger?> GetLedgerById(Guid id, bool includeTransactions, CancellationToken ct)
        {
            IQueryable<Ledger> query = _db.Ledgers
               .AsNoTracking()
               .Include(l => l.Client)
               .Include(l => l.Employee);

            if (includeTransactions)
                query = query.Include(l => l.Transactions);

            return await query.FirstOrDefaultAsync(l => l.Id == id, ct);
        }
    }
}
