using MyLedgerApp.Domain.Entities;

namespace MyLedgerApp.Infrastructure.Repositories
{
    public class LedgerRepositoryMock : ILedgerRepository
    {
        private readonly List<Ledger> _ledgers;
        private readonly Lock _locker = new();

        public LedgerRepositoryMock()
        {
            _ledgers = [];
        }

        public void AddLedger(Ledger ledger)
        {
            lock (_locker)
                _ledgers.Add(ledger);
        }

        public bool DeleteLedger(Ledger ledger)
        {
            lock (_locker)
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
