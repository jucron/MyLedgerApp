using MyLedgerApp.Api.v1.Models;

namespace MyLedgerApp.Domain.Entities.Users
{
    public class Employee: User
    {
        public ServiceCenterType ServiceCenter { get; set; }
        public ICollection<Ledger> Ledgers { get; set; } = [];
    }
}