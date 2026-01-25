using MyLedgerApp.Domain.Entities.Users;
using Shared.Contracts.Events;

namespace MyLedgerApp.Application.Services.Events
{
    public static class EvtMapperExtentions
    {
        public static UserRegisteredEvent ToUserRegisteredEvent(this User user)
        {
            return new UserRegisteredEvent()
            {
               Username = user.Credential.Username,
               Email = user.Email,
               Name = user.Name,
               OccurredAt = DateTime.UtcNow
            };
        }
    }
}
