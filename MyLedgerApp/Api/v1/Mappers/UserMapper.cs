using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Domain.Entities.Users;

namespace MyLedgerApp.Api.v1.Mappers
{
    public class UserMapper
    {
        public static UserDTO MapUserToUserDTO(User user)
        {
            return (user is Client) ?  MapClientToUserDTO(user) : MapEmployeeToUserDTO(user);
        }

        private static UserDTO MapEmployeeToUserDTO(User user)
        {
            Employee employee = (Employee)user;
            return new UserDTO
            {
                Id = employee.Id,
                Username = employee.Credential.Username,
                Name = employee.Name,
                Email = employee.Email,
                UserType = UserType.Employee,
                ServiceCenter = employee.ServiceCenter
            };
        }

        private static UserDTO MapClientToUserDTO(User user)
        {
            Client client = (Client)user;
            return new UserDTO
            {
                Id = client.Id,
                Username = client.Credential.Username,
                Name = client.Name,
                Email = client.Email,
                Ledgers = client.Ledgers.Select(l => l.Id).ToList(),
                UserType = UserType.Client
            };
        }

        public static User MapUserRequestToUser(UserAddRequest userRequest)
        {
            return (userRequest.UserType == UserType.Client) ?
                MapUserRequestToClient(userRequest) : MapUserRequestToEmployee(userRequest);
        }

        private static Employee MapUserRequestToEmployee(UserAddRequest userRequest)
        {
            return new Employee()
            {
                Email = userRequest.Email,
                Name = userRequest.Name,
                ServiceCenter = userRequest.ServiceCenter ?? "not defined.",
                Credential = new Credential(userRequest.Username, userRequest.Password)
            };
        }

        private static Client MapUserRequestToClient(UserAddRequest userRequest)
        {
            return new Client()
            {
                Email = userRequest.Email,
                Name = userRequest.Name,
                Credential = new Credential(userRequest.Username, userRequest.Password)
            };
        }
    }
}
