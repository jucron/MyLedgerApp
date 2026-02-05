using MyLedgerApp.Domain.Entities.Users;
using Shared.Contracts.Events.Publishable;

namespace MyLedgerApp.Domain.Mappers
{
    public static class EventMapper
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
