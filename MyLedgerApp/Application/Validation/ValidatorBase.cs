using FluentValidation;

namespace MyLedgerApp.Application.Validation
{
    public abstract class ValidatorBase<TElement, TValidator> : AbstractValidator<TElement>
        where TValidator : ValidatorBase<TElement, TValidator>, new ()
    {
        public ValidatorBase()
        {
            SetValidations();
        }
        protected abstract void SetValidations();

        public static void Run(TElement element)
        {
            var validator = new TValidator();

            var validationResult = validator.Validate(element);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
        }
    }
}
