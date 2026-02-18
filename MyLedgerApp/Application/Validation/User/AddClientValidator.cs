
using FluentValidation;
using MyLedgerApp.Api.v1.Models;

namespace MyLedgerApp.Application.Validation.User
{
    public class AddClientValidator : ValidatorBase<AddClientRequest, AddClientValidator>
    {
        protected override void SetValidations()
        {
            RuleFor(l => l.Username).NotEmpty();
            RuleFor(l => l.Password).NotEmpty();
            RuleFor(l => l.Email).NotEmpty().EmailAddress();
            RuleFor(l => l.Name).NotEmpty();
        }
    }
}
