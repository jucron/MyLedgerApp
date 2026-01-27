namespace MyLedgerApp.Utils
{
    public class Exceptions
    {
        public class ResourceNotFoundException(string message) : Exception(message)
        {
        }
        public class TransactionNotFoundException(Guid id) :
            ResourceNotFoundException($"Could not found any Transaction with ID {id}")
        {
        }
        public class UserNotFoundException(Guid id) : 
            ResourceNotFoundException($"Could not found any User with ID {id}")
        {
        }
        public class LedgerNotFoundException(Guid id) :
            ResourceNotFoundException($"Could not found any Ledger with ID {id}")
        {
        }
        public class UsernameTakenException(string username) :
            ArgumentException($"Username {username} is already taken")
        {
        }

    }
}
