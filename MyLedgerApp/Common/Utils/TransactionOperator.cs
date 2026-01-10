using MyLedgerApp.Domain.Entities;

namespace MyLedgerApp.Common.Utils
{
    public class TransactionOperator
    {
        public static void ProcessTransaction(Ledger ledger, Transaction transaction)
        {
            if (transaction.Type == TransactionType.Withdrawal )
            {
                if (ledger.CurrentBalance < 0 || ledger.CurrentBalance < transaction.Amount)
                throw new InvalidOperationException("Insufficient funds.");
                ledger.CurrentBalance -= transaction.Amount;
            }
            else if(transaction.Type == TransactionType.Deposit )
            {
                ledger.CurrentBalance += transaction.Amount;
            }
            
        }

        internal static void ProcessTransactionRemoval(Ledger ledger, Transaction transaction)
        {
            // Reversing the processing:

            if (transaction.Type == TransactionType.Deposit)
            {
                // Different from processing, we allow negative amounts.
                ledger.CurrentBalance -= transaction.Amount;
            }
            else if (transaction.Type == TransactionType.Withdrawal)
            {
                ledger.CurrentBalance += transaction.Amount;
            }
        }
    }
}
