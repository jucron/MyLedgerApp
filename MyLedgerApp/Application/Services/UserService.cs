using MyLedgerApp.Api.v1.Mappers;
using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Application.Services.Auth;
using MyLedgerApp.Common.Utils;
using MyLedgerApp.Domain.Entities.Users;
using MyLedgerApp.Infrastructure.DbSessions;
using MyLedgerApp.Infrastructure.Repositories;
using static MyLedgerApp.Common.Utils.Exceptions;

namespace MyLedgerApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IDbSession _dbSession;
        public UserService(IUserRepository userRepository, IDbSession dbSession)
        {
            _userRepository = userRepository;
            _dbSession = dbSession;
        }

        public async Task<UserDTO> AddUser(UserRequest request)
        {
            _ = await _userRepository.GetUserByUsername(request.Username) ??
                throw new UsernameTakenException(request.Username);

            User user = UserMapper.MapUserRequestToUser(request);

            await _userRepository.AddUser(user);
            await _dbSession.SaveChangesAsync();

            return UserMapper.MapUserToUserDTO(user);
        }

        public async Task DeleteUser(Guid id)
        {
            var userToDelete = await _userRepository.GetUserById(id) ??
                throw new UserNotFoundException(id);

            _userRepository.DeleteUser(userToDelete);
            await _dbSession.SaveChangesAsync();
        }

        public async Task<UserDTO> GetUserById(Guid id)
        {
            var userToReturn = await _userRepository.GetUserById(id) ??
                throw new UserNotFoundException(id);

            return UserMapper.MapUserToUserDTO(userToReturn);
        }

        public async Task<IEnumerable<UserDTO>> GetUsers(UserType type)
        {
            var users = await _userRepository.GetAllUsers(type);

            return users.Select(UserMapper.MapUserToUserDTO);
        }

        public async Task<UserDTO> UpdateUser(Guid id, UserDTO user)
        {
            var userToUpdate = await _userRepository.GetUserById(id, isTracking: true) ??
                throw new UserNotFoundException(id);

            bool isModified = false;
            static bool IsDifferent(string? a, string? b) => !string.Equals(a, b, StringComparison.Ordinal);

            TryUtils.ActionIf(IsDifferent(userToUpdate.Name, user.Name), ()=> userToUpdate.Name = user.Name, ref isModified);
            TryUtils.ActionIf(IsDifferent(userToUpdate.Email, user.Email), ()=> userToUpdate.Email = user.Email, ref isModified);

            if (userToUpdate is Employee employee)
                TryUtils.ActionIf(IsDifferent(employee.ServiceCenter, user.ServiceCenter), () => employee.ServiceCenter = user.ServiceCenter!, ref isModified);

            if (isModified)
                await _dbSession.SaveChangesAsync();

            return UserMapper.MapUserToUserDTO(userToUpdate);
        }

     
    }
}