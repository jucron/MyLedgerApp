
namespace MyLedgerApp.Api.v1.Models
{
    public class ClientDTO: UserDTO
    {
        public List<Guid>? Ledgers { get; set; }
    }
}
