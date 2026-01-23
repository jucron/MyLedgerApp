using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Domain.Entities.Users;

namespace MyLedgerApp.Application.Services
{
    public interface IUserService
    {
        Task<UserDTO> AddUser(UserRequest request);
        Task DeleteUser(Guid id);
        Task<UserDTO> GetUserById(Guid id);
        Task<IEnumerable<UserDTO>> GetUsers(UserType type);
        Task<UserDTO> UpdateUser(Guid id, UserDTO user);
    }
}
