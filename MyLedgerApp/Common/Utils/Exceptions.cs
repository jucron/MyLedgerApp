namespace MyLedgerApp.Common.Utils
{
    public class Exceptions
    {
        public class ResourceNotFoundException : Exception
        {
            public ResourceNotFoundException(string message) : base(message)
            {
            }
        }
        public class TransactionNotFoundException : ResourceNotFoundException
        {
            public TransactionNotFoundException(Guid id) 
                : base($"Could not found any Transaction with ID {id}")
            {
            }
        }
        public class UserNotFoundException : ResourceNotFoundException
        {
            public UserNotFoundException(Guid id)
                : base($"Could not found any User with ID {id}")
            {
            }
        }
        public class LedgerNotFoundException : ResourceNotFoundException
        {
            public LedgerNotFoundException(Guid id)
                : base($"Could not found any Ledger with ID {id}")
            {
            }
        }


    }
}
