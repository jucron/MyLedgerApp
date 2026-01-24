using FluentValidation;

namespace MyLedgerApp.Application.Validation
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, TEnum> IsDefinedEnum<T, TEnum>(this IRuleBuilder<T, TEnum> ruleBuilder)
                where TEnum : struct, Enum
        {
            return ruleBuilder
                .Must(value => Enum.IsDefined(value))
                .WithMessage($"Invalid value for enum {typeof(TEnum).Name}");
        }
    }
}
