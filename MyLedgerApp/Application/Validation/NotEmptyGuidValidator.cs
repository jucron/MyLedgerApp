using FluentValidation;

namespace MyLedgerApp.Application.Validation
{
    public class NotEmptyGuidValidator : ValidatorBase<Guid, NotEmptyGuidValidator>
    {
        protected override void SetValidations()
        {
            RuleFor(guid => guid).NotEmpty();
        }
    }
}
