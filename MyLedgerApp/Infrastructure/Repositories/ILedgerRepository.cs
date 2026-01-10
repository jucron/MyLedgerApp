using MyLedgerApp.Domain.Entities;

namespace MyLedgerApp.Infrastructure.Repositories
{
    public interface ILedgerRepository
    {
        IEnumerable<Ledger> GetAllLedgers();
        Ledger? GetLedgerById(Guid id);
        bool DeleteLedger(Ledger ledger);
        void AddLedger(Ledger ledger);
    }
}
