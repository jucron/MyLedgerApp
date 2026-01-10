using MyLedgerApp.Domain.Entities;

namespace MyLedgerApp.Infrastructure.Repositories
{
    public class TransactionRepositoryMock : ITransactionRepository
    {
        private readonly List<Transaction> _transactions;

        public TransactionRepositoryMock()
        {
            _transactions = [];
        }

        public void AddTransaction(Transaction transaction)
        {
           _transactions.Add(transaction);
        }

        public bool DeleteTransaction(Transaction transaction)
        {
            return _transactions.Remove(transaction);
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            return _transactions;
        }

        public Transaction? GetTransactionById(Guid id)
        {
            return _transactions.FirstOrDefault(t => t.Id == id);
        }
        
    }
}
