using MyLedgerApp.Domain;

namespace MyLedgerApp.Repositories
{
    public class TransactionRepositoryMock : ITransactionRepository
    {
        private readonly IEnumerable<Transaction> _transactions;

        public TransactionRepositoryMock()
        {
            _transactions = MockInitialData();
        }

        private static IEnumerable<Transaction> MockInitialData()
        {
            return
            [
                new() { Id = Guid.NewGuid(), Amount = 123.45m, Type = TransactionType.Deposit, Description = "deposit example" },
                new() { Id = Guid.NewGuid(), Amount = 50.20m, Type = TransactionType.Withdrawal, Description = "withdrawal example" },
            ];
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            return _transactions;
        }

        public Transaction? GetTransactionById(Guid id)
        {
            return _transactions.FirstOrDefault(s => s.Id == id);
        }
    }
}
