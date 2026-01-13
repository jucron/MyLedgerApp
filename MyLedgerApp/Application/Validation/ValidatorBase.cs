using FluentValidation;

namespace MyLedgerApp.Application.Validation
{
    /// <summary>
    /// Inherit from this class to create Validators of it's scope.
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    /// <typeparam name="TValidator"></typeparam>
    public abstract class ValidatorBase<TElement, TValidator> : AbstractValidator<TElement>
        where TValidator : ValidatorBase<TElement, TValidator>, new ()
    {
        public ValidatorBase()
        {
            SetValidations();
        }
        protected abstract void SetValidations();

        /// <summary>
        /// Run validation and throw <see cref="ValidationException"/> if not passed.
        /// </summary>
        /// <param name="element"></param>
        /// <exception cref="ValidationException"></exception>
        public static void Run(TElement element)
        {
            var validator = new TValidator();

            var validationResult = validator.Validate(element);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
        }
    }
}
