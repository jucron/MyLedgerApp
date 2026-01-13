
using FluentValidation;
using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Domain.Entities;

namespace MyLedgerApp.Application.Validation.User
{
    public class AddUserValidator : ValidatorBase<UserRequest, AddUserValidator>
    {
        protected override void SetValidations()
        {
            RuleFor(l => l.Username).NotEmpty();
            RuleFor(l => l.Password).NotEmpty();
            RuleFor(l => l.Email).NotEmpty().EmailAddress();
            RuleFor(l => l.Name).NotEmpty();
            RuleFor(l => l.UserType).NotEmpty();
            RuleFor(l => ServiceCenterNecessary(l.ServiceCenter, l.UserType));
        }

        /// <summary>
        /// SC only necessary for employees.
        /// </summary>
        /// <param name="serviceCenter"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        private static bool ServiceCenterNecessary(string? serviceCenter, UserType userType)
        {
            return userType == UserType.Employee && !string.IsNullOrWhiteSpace(serviceCenter);
        }
    }
}
