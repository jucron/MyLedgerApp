
using FluentValidation;
using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Domain.Entities.Users;

namespace MyLedgerApp.Application.Validation.User
{
    public class AddUserValidator : ValidatorBase<UserRequest, AddUserValidator>
    {
        protected override void SetValidations()
        {
            RuleFor(l => l.Username).NotEmpty();
            RuleFor(l => l.Password).NotEmpty();
            RuleFor(l => l.Email).NotEmpty().EmailAddress();
            RuleFor(l => l.Name).NotEmpty();
            RuleFor(l => l.ServiceCenter).NotEmpty()
                .When(l => l.UserType == UserType.Employee)
                .WithMessage("SC necessary for employees.");
        }
    }
}
