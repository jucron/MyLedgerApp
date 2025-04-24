using MyLedgerApp.Api.v1.Mappers;
using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Domain;
using MyLedgerApp.Repositories;
using static MyLedgerApp.MyLedgerAppExceptions;

namespace MyLedgerApp.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public TransactionDTO AddTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public TransactionDTO GetTransactionById(Guid id)
        {
            var transaction = _transactionRepository.GetTransactionById(id) ?? throw new TransactionNotFoundException(id);
            return TransactionMapper.MapTransactionToTransactionDTO(transaction) ;
        }

        public IEnumerable<TransactionDTO> GetTransactions()
        {
            return _transactionRepository.GetAllTransactions()
                .Select(t => TransactionMapper.MapTransactionToTransactionDTO(t));
        }
    }
}