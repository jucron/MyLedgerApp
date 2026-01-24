using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyLedgerApp.Common.Utils;
using MyLedgerApp.Domain.Entities.Users;
using MyLedgerApp.Infrastructure.DbConfig;

namespace MyLedgerApp.Infrastructure.Repositories
{
    public class UserRepository(AppDbContext db) : IUserRepository
    {
        private readonly AppDbContext _db = db;

        public async Task AddUser(User user)
        {
            await _db.AddAsync(user, CTokenHolder.Current);
        }

        public void DeleteUser(User user)
        {
            _db.Remove(user);
        }

        public async Task<IEnumerable<User>> GetAllUsers(UserType type)
        {
            IQueryable<User> query = _db.Users.AsNoTracking();

            query = type switch
            {
                UserType.Client => query.OfType<Client>(),
                _ => query.OfType<Employee>()
            };

            return await query.ToListAsync(CTokenHolder.Current);
        }

        public Task<User?> GetUserById(Guid id, bool isTracking = false)
        {
            var query = isTracking ? _db.Users : _db.Users.AsNoTracking();

            return query.FirstOrDefaultAsync(u => u.Id == id, CTokenHolder.Current);
        }

        public Task<User?> GetUserByUsername(string username, bool isTracking = false)
        {
            var query = isTracking ? _db.Users : _db.Users.AsNoTracking();
            return query
                .FirstOrDefaultAsync(u => u.Credential.Username == username, CTokenHolder.Current);
        }
    }
}
