using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Domain;

namespace MyLedgerApp.Api.v1.Mappers
{
    public class LedgerMapper
    {
        public static LedgerDTO MapLedgerToLedgerDTO(Ledger ledger, bool isFullResponse)
        {
            return new()
            {
                Id = ledger.Id,
                CurrentBalance = ledger.CurrentBalance,
                Transactions = isFullResponse ? ledger.Transactions.Select(t => TransactionMapper.MapTransactionToTransactionDTO(t)).ToList() : null,
                TransactionsId = isFullResponse ? null : ledger.Transactions.Select(t => t.Id).ToList(),
                ClientId = ledger.Client.Id,
                EmployeeId = ledger.Employee.Id
            };
        }
    }
}
