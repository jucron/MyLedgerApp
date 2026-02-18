namespace MyLedgerApp.Api.v1.Models
{
    public class AddEmployeeRequest: AddUserRequest
    {
        public ServiceCenterType ServiceCenter { get; set; }
    }
}
