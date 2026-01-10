using MyLedgerApp.Domain.Entities;

namespace MyLedgerApp.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User? GetUserById(Guid id);
        bool DeleteUser(User user);
        void AddUser(User user);
    }
}
