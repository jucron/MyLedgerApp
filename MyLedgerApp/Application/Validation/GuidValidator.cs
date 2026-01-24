using FluentValidation;

namespace MyLedgerApp.Application.Validation
{
    public class GuidValidator : ValidatorBase<Guid, GuidValidator>
    {
        protected override void SetValidations()
        {
            RuleFor(guid => guid).NotEmpty()
                .WithMessage("Guid cannot be empty");
        }
    }
}
