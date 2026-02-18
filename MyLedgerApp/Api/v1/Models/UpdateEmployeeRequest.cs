namespace MyLedgerApp.Api.v1.Models
{
    public class UpdateEmployeeRequest: UpdateUserRequest
    {

        public ServiceCenterType ServiceCenter {  get; set; }
    }
}
