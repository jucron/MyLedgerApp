using MyLedgerApp.Api.v1.Models;

namespace MyLedgerApp.Application.Services
{
    public interface IUserService
    {
        Task<ClientDTO> AddClient(AddClientRequest request);
        Task<EmployeeDTO> AddEmployee(AddEmployeeRequest request);
        Task<ClientDTO> GetClient(Guid id);
        Task<EmployeeDTO> GetEmployee(Guid id);
        Task DeleteEmployee(Guid id);
        Task DeleteClient(Guid id);
        Task<IEnumerable<ClientDTO>> GetClients();
        Task<IEnumerable<EmployeeDTO>> GetEmployees();
        Task<ClientDTO> UpdateClient(Guid id, UpdateClientRequest userUpReq);
        Task<EmployeeDTO> UpdateEmployee(Guid id, UpdateEmployeeRequest userUpReq);
    }
}
