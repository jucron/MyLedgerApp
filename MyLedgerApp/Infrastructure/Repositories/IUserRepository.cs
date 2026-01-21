using MyLedgerApp.Domain.Entities;

namespace MyLedgerApp.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers(CancellationToken ct);
        Task<User?> GetUserById(Guid id, CancellationToken ct);
        Task<User?> GetUserByUsername(string username, CancellationToken ct);
        Task DeleteUser(User user, CancellationToken ct);
        Task AddUser(User user, CancellationToken ct);
        Task UpdateUser(User user, CancellationToken ct);
    }
}
