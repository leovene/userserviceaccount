using FluentValidation;
using UserServiceAccount.Domain.Entities;

namespace UserServiceAccount.Business.Validators
{
    public class UserValidator : AbstractValidator<UserEntity>
    {
        public UserValidator() : base()
        {
            RuleFor(m => m.Id)
                .NotEmpty().WithMessage("Id is required")
                .NotNull().WithMessage("Id is required");

            RuleFor(m => m.UserName)
                .NotEmpty().WithMessage("UserName is required")
                .NotNull().WithMessage("UserName is required");

            RuleFor(m => m.Password)
                .NotEmpty().WithMessage("Password is required")
                .NotNull().WithMessage("Password is required");
        }
    }
}
