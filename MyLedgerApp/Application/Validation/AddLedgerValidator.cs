
using FluentValidation;
using MyLedgerApp.Api.v1.Models;

namespace MyLedgerApp.Application.Validation.User
{
    public class AddLedgerValidator : ValidatorBase<LedgerRequest, AddLedgerValidator>
    {
        protected override void SetValidations()
        {
            RuleFor(l => l.EmployeeId).NotEmpty();
            RuleFor(l => l.ClientId).NotEmpty();
        }

    }
}
