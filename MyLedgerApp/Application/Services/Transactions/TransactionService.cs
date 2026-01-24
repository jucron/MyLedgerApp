using MyLedgerApp.Api.v1.Mappers;
using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Domain.Entities;
using MyLedgerApp.Infrastructure.DbSessions;
using MyLedgerApp.Infrastructure.Repositories;
using static MyLedgerApp.Common.Utils.Exceptions;

namespace MyLedgerApp.Application.Services.Transactions
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILedgerRepository _ledgerRepository;
        private readonly IDbSession _dbSession;
        public TransactionService(ITransactionRepository transactionRepository, ILedgerRepository ledgerRepository, IDbSession session)
        {
            _transactionRepository = transactionRepository;
            _ledgerRepository = ledgerRepository;
            _dbSession = session;
        }

        public async Task<TransactionDTO> AddTransaction(TransactionRequest request)
        {
            var ledger = await _ledgerRepository.GetLedgerById(request.LedgerId, includeTransactions: true, isTracking: true) ??
                throw new LedgerNotFoundException(request.LedgerId);

            Transaction newTransaction = new()
            {
                Amount = request.Amount,
                Description = request.Description,
                Type = request.Type,
                LedgerId = ledger.Id
            };

            LedgerBalanceManager.AddTransaction(ledger, newTransaction);

            await _transactionRepository.AddTransaction(newTransaction);
            ledger.Transactions.Add(newTransaction);
            await _dbSession.SaveChangesAsync();
            
            return TransactionMapper.MapTransactionToTransactionDTO(newTransaction);
            
        }

        public async Task DeleteTransaction(Guid id)
        {
            var transactionToDelete = await _transactionRepository.GetTransactionById(id, isTracking: true) 
                ?? throw new TransactionNotFoundException(id);

            var ledger = await _ledgerRepository.GetLedgerById(transactionToDelete.LedgerId, includeTransactions: true, isTracking: true) ??
                throw new LedgerNotFoundException(transactionToDelete.LedgerId);

            LedgerBalanceManager.RemoveTransaction(ledger, transactionToDelete);

            ledger.Transactions.Remove(transactionToDelete);
            _transactionRepository.DeleteTransaction(transactionToDelete);
            await _dbSession.SaveChangesAsync();
        }

        public async Task<TransactionDTO> GetTransactionById(Guid id)
        {
            var transaction = await _transactionRepository.GetTransactionById(id) ?? 
                throw new TransactionNotFoundException(id);

            return TransactionMapper.MapTransactionToTransactionDTO(transaction) ;
        }

        public async Task<IEnumerable<TransactionDTO>> GetTransactions(Guid clientId)
        {
            var transactions = await _transactionRepository.GetTransactionsByClientId(clientId);
            return transactions.Select(TransactionMapper.MapTransactionToTransactionDTO);
        }
    }
}