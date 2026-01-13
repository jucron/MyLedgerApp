
using FluentValidation;
using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Domain.Entities;

namespace MyLedgerApp.Application.Validation.User
{
    public class UpdateUserValidator : ValidatorBase<UserDTO, UpdateUserValidator>
    {
        protected override void SetValidations()
        {
            RuleFor(l => l.Email).EmailAddress()
                .When(l => !string.IsNullOrWhiteSpace(l.Email));

            RuleFor(l => l.UserType).Empty()
                .WithMessage("You cannot change UserType after the account is created.");

            RuleFor(l => l.ServiceCenter).Empty()
                .When(l => l.UserType is UserType.Client)
                .WithMessage("Clients cannot have SCs.");
        }
    }
}
