namespace MyLedgerApp.Application.Validation
{
    public class NotNullEnumValidator : ValidatorBase<Enum, NotNullEnumValidator>
    {
        protected override void SetValidations()
        {
            RuleFor(x => NotNull(x));
        }

        private static bool NotNull(Enum x)
        {
            return x is not null;
        }
    }
}
