using FluentValidation;

namespace MyLedgerApp.Application.Validation
{
    public class EnumValidator<TEnum> : ValidatorBase<TEnum, EnumValidator<TEnum>> 
        where TEnum : struct, Enum
    {
        protected override void SetValidations()
        {
            RuleFor(x => x)
            .IsDefinedEnum();
        }
    }
}
