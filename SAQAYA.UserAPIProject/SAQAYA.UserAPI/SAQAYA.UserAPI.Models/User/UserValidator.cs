using FluentValidation;

namespace SAQAYA.UserAPI.Models
{
    public class UserValidator : AbstractValidator<UserModel>
    {
        public UserValidator()
        {
            RuleFor(x => x.Id);
            RuleFor(x => x.FirstName).NotNull().MaximumLength(100);
            RuleFor(x => x.LastName).NotNull().MaximumLength(100);
            RuleFor(x => x.Email).NotNull().MaximumLength(100).EmailAddress();
            RuleFor(x => x.MarketingConsent).NotNull();
        }
    }
}
