using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyLedgerApp.Domain.Entities;
using MyLedgerApp.Infrastructure.DbConfig;

namespace MyLedgerApp.Infrastructure.Repositories
{
    public class UserRepository(AppDbContext db) : IUserRepository
    {
        private readonly AppDbContext _db = db;

        public async Task AddUser(User user, CancellationToken ct)
        {
            await _db.AddAsync(user, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteUser(User user, CancellationToken ct)
        {
            _db.Remove(user);
            await _db.SaveChangesAsync(ct);
        }

        public async Task<IEnumerable<User>> GetAllUsers(CancellationToken ct)
        {
            return await _db.Users.AsNoTracking().ToListAsync(ct);
        }

        public Task<User?> GetUserById(Guid id, CancellationToken ct)
        {
            return _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id, ct);
        }

        public Task<User?> GetUserByUsername(string username, CancellationToken ct)
        {
            return _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Credential.Username == username, ct);
        }
        public async Task UpdateUser(User user, CancellationToken ct)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync(ct);
        }

    }
}
