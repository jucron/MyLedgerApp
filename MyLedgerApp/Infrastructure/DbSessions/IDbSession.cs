namespace MyLedgerApp.Infrastructure.DbSessions
{
    public interface IDbSession
    {
        Task SaveChangesAsync(DBExceptionContext? dbExContext = null);
    }
}
