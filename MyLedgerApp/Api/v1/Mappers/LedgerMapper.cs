using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Domain.Entities;

namespace MyLedgerApp.Api.v1.Mappers
{
    public class LedgerMapper
    {
        public static LedgerDTO MapLedgerToLedgerDTO(Ledger ledger)
        {
            return new()
            {
                Id = ledger.Id,
                CurrentBalance = ledger.CurrentBalance,
                Transactions = ledger.Transactions?.Select(TransactionMapper.MapTransactionToTransactionDTO).ToList(),
                ClientId = ledger.Client.Id,
                EmployeeId = ledger.Employee.Id
            };
        }
    }
}
