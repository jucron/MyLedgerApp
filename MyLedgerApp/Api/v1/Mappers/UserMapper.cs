using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Domain.Entities.Users;

namespace MyLedgerApp.Api.v1.Mappers
{
    public class UserMapper
    {
        public static EmployeeDTO MapEmployeeToDTO(Employee employee)
        {
            return new EmployeeDTO
            {
                Id = employee.Id,
                Username = employee.Credential.Username,
                Name = employee.Name,
                Email = employee.Email,
                ServiceCenter = employee.ServiceCenter
            };
        }

        public static ClientDTO MapClientToDTO(Client client)
        {
            return new ClientDTO
            {
                Id = client.Id,
                Username = client.Credential.Username,
                Name = client.Name,
                Email = client.Email,
                Ledgers = client.Ledgers.Select(l => l.Id).ToList(),
            };
        }

        public static Employee MapEmployeeRequestToEmployee(AddEmployeeRequest userRequest)
        {
            return new Employee()
            {
                Email = userRequest.Email,
                Name = userRequest.Name,
                ServiceCenter = userRequest.ServiceCenter,
                Credential = new Credential(userRequest.Username, userRequest.Password)
            };
        }
        public static Client MapClientRequestToClient(AddClientRequest userRequest)
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
