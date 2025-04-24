namespace MyLedgerApp
{
    public class MyLedgerAppExceptions
    {
        public class TransactionNotFoundException : Exception
        {
            public TransactionNotFoundException(Guid id)
                : base($"Could not found Transaction with ID {id}")
            {
            }
        }
    }
}
