
using MyLedgerApp.Common.Utils;
using MyLedgerApp.Infrastructure.DbConfig;

namespace MyLedgerApp.Infrastructure.DbSessions
{
    public class DbSession(AppDbContext dbContext) : IDbSession
    {
        private readonly AppDbContext _db = dbContext;
        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync(CTokenHolder.Current);
        }
    }
}
