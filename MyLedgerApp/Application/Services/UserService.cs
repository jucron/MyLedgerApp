using MyLedgerApp.Api.v1.Mappers;
using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Domain.Entities.Users;
using MyLedgerApp.Domain.Mappers;
using MyLedgerApp.Infrastructure.DbSessions;
using MyLedgerApp.Infrastructure.Repositories;
using MyLedgerApp.Utils;
using Shared.Contracts.Events;
using static MyLedgerApp.Utils.Exceptions;

namespace MyLedgerApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IDbSession _dbSession;
        private readonly IEventPublisher _evtPublisher;

        private static bool IsDifString(string? a, string? b) => !string.Equals(a, b, StringComparison.Ordinal);
        private static bool IsDifEnum(Enum? a, Enum? b) => !Enum.Equals(a, b);

        public UserService(IUserRepository userRepository, IDbSession dbSession, IEventPublisher evtPub)
        {
            _userRepository = userRepository;
            _dbSession = dbSession;
            _evtPublisher = evtPub;
        }

        public async Task<ClientDTO> AddClient(AddClientRequest request)
        {
            var client = UserMapper.MapClientRequestToClient(request);
            await AddUser(client);
            return UserMapper.MapClientToDTO(client);
        }

        public async Task<EmployeeDTO> AddEmployee(AddEmployeeRequest request)
        {
            var employee = UserMapper.MapEmployeeRequestToEmployee(request);
            await AddUser(employee);
            return UserMapper.MapEmployeeToDTO(employee);
        }
        private async Task<User> AddUser(User user)
        {
            await _userRepository.AddUser(user);

            var context = new DBExceptionContext() 
            { Username = user.Credential.Username, Email = user.Email };

            await _dbSession.SaveChangesAsync(context);

            _ = _evtPublisher.PublishAsync(user.ToUserRegisteredEvent()); // Fire forget for now

            return user;
        }

        public async Task DeleteClient(Guid id)
        {
            var userToDelete = await _userRepository.GetUserById(id);

            if (userToDelete is not null && userToDelete is Client clientToDelete)
            {
                _userRepository.DeleteUser(clientToDelete);
                await _dbSession.SaveChangesAsync();
                return;
            }

            throw new UserNotFoundException(id);
        }

        public async Task DeleteEmployee(Guid id)
        {
            var userToDelete = await _userRepository.GetUserById(id);

            if (userToDelete is not null && userToDelete is Employee employeeToDelete)
            {
                _userRepository.DeleteUser(employeeToDelete);
                await _dbSession.SaveChangesAsync();
                return;
            }

            throw new UserNotFoundException(id);
        }

        public async Task<ClientDTO> GetClient(Guid id)
        {
            var userToReturn = await _userRepository.GetUserById(id);

            if (userToReturn is not null && userToReturn is Client client)
                return UserMapper.MapClientToDTO(client);
                
            throw new UserNotFoundException(id);
        }

        public async Task<EmployeeDTO> GetEmployee(Guid id)
        {
            var userToReturn = await _userRepository.GetUserById(id);

            if (userToReturn is not null && userToReturn is Employee employee)
                return UserMapper.MapEmployeeToDTO(employee);

            throw new UserNotFoundException(id);
        }

        public async Task<IEnumerable<ClientDTO>> GetClients()
        {
            var users = await _userRepository.GetClients();

            return users.Select(UserMapper.MapClientToDTO);
        }
        public async Task<IEnumerable<EmployeeDTO>> GetEmployees()
        {
            var users = await _userRepository.GetEmployees();

            return users.Select(UserMapper.MapEmployeeToDTO);
        }
        public async Task<ClientDTO> UpdateClient(Guid id, UpdateClientRequest userUpReq)
        {
            var user = await _userRepository.GetUserById(id, isTracking: true);

            if (user is not null && user is Client client)
            {
                bool isModified = false;

                TryUtils.ActionIf(IsDifString(client.Name, userUpReq.Name), () => client.Name = userUpReq.Name, ref isModified);
                TryUtils.ActionIf(IsDifString(client.Email, userUpReq.Email), () => client.Email = userUpReq.Email, ref isModified);

                if (isModified)
                    await _dbSession.SaveChangesAsync();

                return UserMapper.MapClientToDTO(client);
            }

            throw new UserNotFoundException(id);
        }

        public async Task<EmployeeDTO> UpdateEmployee(Guid id, UpdateEmployeeRequest userUpReq)
        {
            var user = await _userRepository.GetUserById(id, isTracking: true);

            if (user is not null && user is Employee employee)
            {
                bool isModified = false;

                TryUtils.ActionIf(IsDifString(employee.Name, userUpReq.Name), () => employee.Name = userUpReq.Name, ref isModified);
                TryUtils.ActionIf(IsDifString(employee.Email, userUpReq.Email), () => employee.Email = userUpReq.Email, ref isModified);
                TryUtils.ActionIf(IsDifEnum(employee.ServiceCenter, userUpReq.ServiceCenter), () => employee.ServiceCenter = userUpReq.ServiceCenter, ref isModified);

                if (isModified)
                    await _dbSession.SaveChangesAsync();

                return UserMapper.MapEmployeeToDTO(employee);
            }

            throw new UserNotFoundException(id);
        }
     
    }
}