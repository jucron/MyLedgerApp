using MyLedgerApp.Domain.Entities;

namespace MyLedgerApp.Infrastructure.Repositories
{
    public class UserRepositoryMock : IUserRepository
    {
        private readonly List<User> _users;

        public UserRepositoryMock()
        {
            _users = MockInitialData();
        }

        private static List<User> MockInitialData()
        {
            return
            [
                new Client { Email = "john@email.com", Name = "John"},
                new Employee { Email = "larry@email.com", Name = "Larry", serviceCenter = "NY"},
            ];
        }

        public bool DeleteUser(User user)
        {
            return _users.Remove(user);
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
            _users.Add(user);
        }
    }
}
