
namespace MyLedgerApp.Api.v1.Models
{
    public class EmployeeDTO: UserDTO
    {
        public ServiceCenterType ServiceCenter { get; set; }
    }
}
