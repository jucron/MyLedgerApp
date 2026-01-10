using MyLedgerApp.Domain.Entities;

namespace MyLedgerApp.Api.v1.Models
{
    public class UserRequest
    {

        public required string Name { get; set; }

        public required string Email { get; set; }

        public UserType UserType { get; set; }

        public string? ServiceCenter {  get; set; }
    }
}
