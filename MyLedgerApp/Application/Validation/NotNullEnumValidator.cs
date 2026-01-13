using FluentValidation;

namespace MyLedgerApp.Application.Validation
{
    public class NotNullEnumValidator : ValidatorBase<Enum, NotNullEnumValidator>
    {
        protected override void SetValidations()
        {
            RuleFor(x => x)
                .Must(NotNull)
                .WithMessage("Enum must not be null");
        }

        private static bool NotNull(Enum x)
        {
            return x is not null;
        }
    }
}
