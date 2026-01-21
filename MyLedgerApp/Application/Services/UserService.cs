using MyLedgerApp.Api.v1.Mappers;
using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Application.Services.Auth;
using MyLedgerApp.Domain.Entities;
using MyLedgerApp.Infrastructure.Repositories;
using static MyLedgerApp.Common.Utils.Exceptions;

namespace MyLedgerApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> AddUser(UserRequest request, CancellationToken ct)
        {
            //todo: check if user already exists
            User user = UserMapper.MapUserRequestToUser(request);
            await _userRepository.AddUser(user, ct);
            return UserMapper.MapUserToUserDTO(user);
        }

        public async Task DeleteUser(Guid id, CancellationToken ct)
        {
            var userToDelete = await _userRepository.GetUserById(id, ct) ??
                throw new UserNotFoundException(id);

            await _userRepository.DeleteUser(userToDelete, ct);
        }

        public async Task<UserDTO> GetUserById(Guid id, CancellationToken ct)
        {
            var userToReturn = await _userRepository.GetUserById(id, ct) ??
                throw new UserNotFoundException(id);

            return UserMapper.MapUserToUserDTO(userToReturn);
        }

        public async Task<IEnumerable<UserDTO>> GetUsers(UserType type, CancellationToken ct)
        {
            var users = await _userRepository.GetAllUsers(ct);

            Func<User,bool> isClientOrIsEployee = type is UserType.Client ? (u) => u is Client : (u) => u is Employee;

            return users
                .Where(isClientOrIsEployee)
                .Select(UserMapper.MapUserToUserDTO);
        }

        public async Task<UserDTO> UpdateUser(Guid id, UserDTO user, CancellationToken ct)
        {
            var userToUpdate = await _userRepository.GetUserById(id, ct) ??
                throw new UserNotFoundException(id);

            userToUpdate.Name = user.Name;
            userToUpdate.Email = user.Email;

            if (userToUpdate is Employee employee)
                employee.ServiceCenter = user.ServiceCenter ?? employee.ServiceCenter;
            
            await _userRepository.UpdateUser(userToUpdate, ct);

            return UserMapper.MapUserToUserDTO(userToUpdate);
        }

     
    }
}