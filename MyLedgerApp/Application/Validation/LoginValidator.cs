
using MyLedgerApp.Api.v1.Models;

namespace MyLedgerApp.Application.Validation
{
    public class LoginValidator : ValidatorBase<LoginRequest, LoginValidator>
    {
        protected override void SetValidations()
        {
            RuleFor(l => HasUsername(l));
            RuleFor(l => HasPassword(l));
        }

        private static bool HasPassword(LoginRequest l)
        {
            return !string.IsNullOrEmpty(l.Password);
        }

        private static bool HasUsername(LoginRequest l)
        {
            return !string.IsNullOrEmpty(l.Username);
        }
    }
}
