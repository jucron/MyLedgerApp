using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Domain.Entities;

namespace MyLedgerApp.Application.Services
{
    public interface IUserService
    {
        Task<UserDTO> AddUser(UserRequest request, CancellationToken ct);
        Task DeleteUser(Guid id, CancellationToken ct);
        Task<UserDTO> GetUserById(Guid id, CancellationToken ct);
        Task<IEnumerable<UserDTO>> GetUsers(UserType type, CancellationToken ct);
        Task<UserDTO> UpdateUser(Guid id, UserDTO user, CancellationToken ct);
    }
}
