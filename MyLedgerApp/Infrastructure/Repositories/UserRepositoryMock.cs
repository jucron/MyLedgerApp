using MyLedgerApp.Domain.Entities;

namespace MyLedgerApp.Infrastructure.Repositories
{
    public class UserRepositoryMock : IUserRepository
    {
        private readonly List<User> _users;
        private readonly Lock _locker = new();

        public UserRepositoryMock()
        {
            _users = MockInitialData();
        }

        private static List<User> MockInitialData()
        {
            var hashPass123 = "$2a$11$z5mW0e5m8AGiIymcDLUUae7XIZLUnm4zz8uol8asbLYHwy0bUncLW";
            return
            [
                new Client { Email = "john@email.com", Name = "John", Credential = new() { Username = "john", PasswordHash = hashPass123 } },
                new Employee { Email = "larry@email.com", Name = "Larry", ServiceCenter = "NY", Credential = new() { Username = "larry", PasswordHash = hashPass123 }},
            ];
        }

        public bool DeleteUser(User user)
        {
            lock (_locker)
            {
                return _users.Remove(user);
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _users;
        }

        public User? GetUserById(Guid id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public void AddUser(User user)
        {
            lock (_locker)
            {
                _users.Add(user);
            }
        }

        public User? GetUserByUsername(string username)
        {
            return _users.FirstOrDefault(u =>
                string.Equals(u.Name, username, StringComparison.OrdinalIgnoreCase));
        }
    }
}
