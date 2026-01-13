using MyLedgerApp.Api.v1.Mappers;
using MyLedgerApp.Api.v1.Models;
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

        public UserDTO AddUser(UserRequest request)
        {
            User user = UserMapper.MapUserRequestToUser(request);
            _userRepository.AddUser(user);
            return UserMapper.MapUserToUserDTO(user);
        }

        public void DeleteUser(Guid id)
        {
            var userToDelete = _userRepository.GetUserById(id) ??
                throw new UserNotFoundException(id);

            _userRepository.DeleteUser(userToDelete);
        }

        public UserDTO GetUserById(Guid id)
        {
            var userToReturn = _userRepository.GetUserById(id) ??
                throw new UserNotFoundException(id);

            return UserMapper.MapUserToUserDTO(userToReturn);
        }

        public IEnumerable<UserDTO> GetUsers(UserType type)
        {
            bool isClient = type is UserType.Client;

            return _userRepository.GetAllUsers()
                .Where(u => isClient ? u is Client : u is Employee)
                .Select(UserMapper.MapUserToUserDTO);
        }

        public UserDTO UpdateUser(Guid id, UserDTO user)
        {
            // Note that User's Ledgers should be edited by the specific api

            var userToUpdate = _userRepository.GetUserById(id) ??
                throw new UserNotFoundException(id);

            userToUpdate.Name = user.Name;
            userToUpdate.Email = user.Email;

            if (userToUpdate is Employee employee)
            {
                employee.serviceCenter = user.ServiceCenter ?? employee.serviceCenter;
            }

            return UserMapper.MapUserToUserDTO(userToUpdate);
        }

     
    }
}