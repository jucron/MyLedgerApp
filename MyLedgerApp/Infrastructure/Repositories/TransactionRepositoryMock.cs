using MyLedgerApp.Domain.Entities;

namespace MyLedgerApp.Infrastructure.Repositories
{
    public class TransactionRepositoryMock : ITransactionRepository
    {
        private readonly List<Transaction> _transactions;
        private readonly Lock _locker = new();

        public TransactionRepositoryMock()
        {
            _transactions = [];
        }

        public void AddTransaction(Transaction transaction)
        {
            lock (_locker)
            {
                _transactions.Add(transaction);
            }
        }

        public bool DeleteTransaction(Transaction transaction)
        {
            lock (_locker)
            {
                return _transactions.Remove(transaction);
            }
        }

        public IEnumerable<Transaction> GetTransactionsByClientId(Guid clientId)
        {
            return _transactions
                .Select(t => t)
                .Where(t => t.Client.Id.Equals(clientId))
                .ToList();
        }

        public Transaction? GetTransactionById(Guid id)
        {
            return _transactions.FirstOrDefault(t => t.Id == id);
        }

    }
}
