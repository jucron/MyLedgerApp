using MyLedgerApp.Domain.Entities;

namespace MyLedgerApp.Api.v1.Models
{
    public class UserDTO
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }
        public required string Username { get; set; }

        public required string Email { get; set; }

        public UserType UserType { get; set; }

        public List<Guid>? Ledgers { get; set; }
        public string? ServiceCenter {  get; set; }
    }
}
