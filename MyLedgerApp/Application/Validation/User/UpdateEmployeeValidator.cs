
using FluentValidation;
using MyLedgerApp.Api.v1.Models;

namespace MyLedgerApp.Application.Validation.User
{
    public class UpdateEmployeeValidator : ValidatorBase<UpdateEmployeeRequest, UpdateEmployeeValidator>
    {
        protected override void SetValidations()
        {
            RuleFor(l => l.Email).EmailAddress()
                .When(l => !string.IsNullOrWhiteSpace(l.Email));

            RuleFor(l => l.ServiceCenter).IsDefinedEnum();
        }
    }
}
