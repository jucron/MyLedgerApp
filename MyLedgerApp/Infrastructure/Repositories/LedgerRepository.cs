using Microsoft.EntityFrameworkCore;
using MyLedgerApp.Common.Utils;
using MyLedgerApp.Domain.Entities;
using MyLedgerApp.Infrastructure.DbConfig;

namespace MyLedgerApp.Infrastructure.Repositories
{
    public class LedgerRepository(AppDbContext db) : ILedgerRepository
    {
        private readonly AppDbContext _db = db;

        public async Task AddLedger(Ledger ledger) 
        {
            await _db.AddAsync(ledger, CTokenHolder.Current);
        }

        public void DeleteLedger(Ledger ledger)
        {
            _db.Remove(ledger);
        }

        public async Task<IEnumerable<Ledger>> GetAllLedgers(bool includeTransactions)
        {
            IQueryable<Ledger> query = _db.Ledgers
                .AsNoTracking()
                .Include(l => l.Client)
                .Include(l => l.Employee);

            if (includeTransactions)
                query = query.Include(l => l.Transactions);

            return await query.ToListAsync(CTokenHolder.Current);
        }

        public async Task<Ledger?> GetLedgerById(Guid id, bool includeTransactions, bool isTracking = false)
        {
            IQueryable<Ledger> query = isTracking ? _db.Ledgers : _db.Ledgers.AsNoTracking();

            query = query
                .Include(l => l.Client)
                .Include(l => l.Employee);

            if (includeTransactions)
                query = query.Include(l => l.Transactions);

            return await query.FirstOrDefaultAsync(l => l.Id == id, CTokenHolder.Current);
        }
    }
}
