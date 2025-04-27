using MyLedgerApp.Domain;

namespace MyLedgerApp.Repositories
{
    public interface ILedgerRepository
    {
        IEnumerable<Ledger> GetAllLedgers();
        Ledger? GetLedgerById(Guid id);
        bool DeleteLedger(Ledger ledger);
        void AddLedger(Ledger ledger);
    }
}
