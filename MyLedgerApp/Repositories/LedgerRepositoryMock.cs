using MyLedgerApp.Domain;

namespace MyLedgerApp.Repositories
{
    public class LedgerRepositoryMock : ILedgerRepository
    {
        private readonly List<Ledger> _ledgers;

        public LedgerRepositoryMock()
        {
            _ledgers = [];
        }

        public void AddLedger(Ledger ledger)
        {
            _ledgers.Add(ledger);
        }

        public bool DeleteLedger(Ledger ledger)
        {
           return _ledgers.Remove(ledger);
        }

        public IEnumerable<Ledger> GetAllLedgers()
        {
            return _ledgers;
        }

        public Ledger? GetLedgerById(Guid id)
        {
            return _ledgers.FirstOrDefault(u => u.Id == id);
        }

    }
}
