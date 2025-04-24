using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Domain;

namespace MyLedgerApp.Api.v1.Mappers
{
    public class TransactionMapper
    {
        public static TransactionDTO MapTransactionToTransactionDTO(Transaction transaction)
        {
            return new TransactionDTO
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                Description = transaction.Description,
                Timestamp = transaction.Timestamp,
                Type = transaction.Type,
                ClientName = transaction.Client.Name,
                EmployeeName = transaction.Employee.Name
            };
        }
    }
}
