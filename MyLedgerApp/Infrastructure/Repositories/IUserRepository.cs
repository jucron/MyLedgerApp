using MyLedgerApp.Domain.Entities.Users;

namespace MyLedgerApp.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers(UserType type);
        Task<User?> GetUserById(Guid id, bool isTracking = false);
        Task<User?> GetUserByUsername(string username, bool isTracking = false);
        void DeleteUser(User user);
        Task AddUser(User user);
    }
}
