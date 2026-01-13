
using FluentValidation;
using MyLedgerApp.Api.v1.Models;

namespace MyLedgerApp.Application.Validation
{
    public class LoginValidator : ValidatorBase<LoginRequest, LoginValidator>
    {
        protected override void SetValidations()
        {
            RuleFor(l => l.Username).NotEmpty();
            RuleFor(l => l.Password).NotEmpty();
        }
    }
}
