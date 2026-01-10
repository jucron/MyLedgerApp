using MyLedgerApp.Api.v1.Mappers;
using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Common.Utils;
using MyLedgerApp.Domain.Entities;
using MyLedgerApp.Infrastructure.Repositories;
using static MyLedgerApp.Common.Utils.Exceptions;

namespace MyLedgerApp.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILedgerRepository _ledgerRepository;
        public TransactionService(ITransactionRepository transactionRepository, ILedgerRepository ledgerRepository)
        {
            _transactionRepository = transactionRepository;
            _ledgerRepository = ledgerRepository;
        }

        public TransactionDTO AddTransaction(TransactionRequest request)
        {
            var ledger = _ledgerRepository.GetLedgerById(request.LedgerId) ??
                throw new LedgerNotFoundException(request.LedgerId);

            Transaction newTransaction = new()
            {
                Amount = request.Amount,
                Description = request.Description,
                Type = request.Type,
                Client = ledger.Client,
                Ledger = ledger
            };

            TransactionOperator.ProcessTransaction(ledger, newTransaction);
            _transactionRepository.AddTransaction(newTransaction);
            ledger.Transactions.Add(newTransaction);
            
            return TransactionMapper.MapTransactionToTransactionDTO(newTransaction);
            
        }

        public void DeleteTransaction(Guid id)
        {
            var transactionToDelete = _transactionRepository.GetTransactionById(id) 
                ?? throw new TransactionNotFoundException(id);

            var ledger = _ledgerRepository.GetLedgerById(transactionToDelete.Ledger.Id) ??
                throw new LedgerNotFoundException(transactionToDelete.Ledger.Id);

            TransactionOperator.ProcessTransactionRemoval(ledger, transactionToDelete);

            ledger.Transactions.Remove(transactionToDelete);
            _transactionRepository.DeleteTransaction(transactionToDelete);
        }

        public TransactionDTO GetTransactionById(Guid id)
        {
            var transaction = _transactionRepository.GetTransactionById(id) ?? 
                throw new TransactionNotFoundException(id);

            return TransactionMapper.MapTransactionToTransactionDTO(transaction) ;
        }

        public IEnumerable<TransactionDTO> GetAllTransactions()
        {
            return _transactionRepository.GetAllTransactions()
                .Select(t => TransactionMapper.MapTransactionToTransactionDTO(t));
        }
    }
}