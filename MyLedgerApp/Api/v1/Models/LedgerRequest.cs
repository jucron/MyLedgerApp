namespace MyLedgerApp.Api.v1.Models
{
    public class LedgerRequest
    {
        public Guid ClientId { get; set; }
        public Guid EmployeeId { get; set; }

    }
}