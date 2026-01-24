
using FluentValidation;
using MyLedgerApp.Api.v1.Models;

namespace MyLedgerApp.Application.Validation.User
{
    public class AddTransactionValidator : ValidatorBase<TransactionRequest, AddTransactionValidator>
    {
        protected override void SetValidations()
        {
            RuleFor(t => t.LedgerId).NotEmpty();
            RuleFor(t => t.Amount).NotEmpty();
            RuleFor(t => t.Description).NotEmpty();
            RuleFor(t => t.Type).IsDefinedEnum();
        }

    }
}
