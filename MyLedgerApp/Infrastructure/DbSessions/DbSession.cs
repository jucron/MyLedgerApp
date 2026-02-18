using MyLedgerApp.Infrastructure.DbConfig;
using MyLedgerApp.Utils;

namespace MyLedgerApp.Infrastructure.DbSessions
{
    public class DbSession(AppDbContext dbContext) : IDbSession
    {
        private readonly AppDbContext _db = dbContext;
        public async Task SaveChangesAsync(DBExceptionContext? dbExContext = null)
        {
            try
            {
                await _db.SaveChangesAsync(CTokenHolder.Current);
            }
            catch (Exception ex)
            {
                throw DbExceptionTranslator.Translate(ex, dbExContext);
            }
        }
    }
}
