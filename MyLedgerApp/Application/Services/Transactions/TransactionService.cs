using MyLedgerApp.Api.v1.Mappers;
using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Domain.Entities;
using MyLedgerApp.Infrastructure.Repositories;
using static MyLedgerApp.Common.Utils.Exceptions;

namespace MyLedgerApp.Application.Services.Transactions
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

        public async Task<TransactionDTO> AddTransaction(TransactionRequest request, CancellationToken ct)
        {
            var ledger = await _ledgerRepository.GetLedgerById(request.LedgerId, false, ct) ??
                throw new LedgerNotFoundException(request.LedgerId);

            Transaction newTransaction = new()
            {
                Amount = request.Amount,
                Description = request.Description,
                Type = request.Type,
                LedgerId = ledger.Id
            };

            TransactionOperator.ProcessTransaction(ledger, newTransaction);
            await _transactionRepository.AddTransaction(newTransaction, ct);
            await _ledgerRepository.UpdateLedger(ledger, ct);
            ledger.Transactions.Add(newTransaction);
            
            return TransactionMapper.MapTransactionToTransactionDTO(newTransaction);
            
        }

        public async Task DeleteTransaction(Guid id, CancellationToken ct)
        {
            var transactionToDelete = _transactionRepository.GetTransactionById(id, ct) 
                ?? throw new TransactionNotFoundException(id);

            var ledger = _ledgerRepository.GetLedgerById(transactionToDelete.Ledger.Id) ??
                throw new LedgerNotFoundException(transactionToDelete.Ledger.Id);

            TransactionOperator.ProcessTransactionRemoval(ledger, transactionToDelete);

            ledger.Transactions.Remove(transactionToDelete);
            _transactionRepository.DeleteTransaction(transactionToDelete);
        }

        public async Task<TransactionDTO> GetTransactionById(Guid id, CancellationToken ct)
        {
            var transaction = _transactionRepository.GetTransactionById(id) ?? 
                throw new TransactionNotFoundException(id);

            return TransactionMapper.MapTransactionToTransactionDTO(transaction) ;
        }

        public async Task<IEnumerable<TransactionDTO>> GetTransactions(Guid clientId, CancellationToken ct)
        {
            return _transactionRepository.GetTransactionsByClientId(clientId)
                .Select(t => TransactionMapper.MapTransactionToTransactionDTO(t));
        }
    }
}