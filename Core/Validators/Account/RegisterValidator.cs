using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Domain.Entities.Identity;
using Core.Models.Account;

namespace Core.Validators.Account;

public class RegisterValidator : AbstractValidator<RegisterModel>
{
    // Порожній конструктор для EF / design-time
    public RegisterValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Incorrect format of email");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password should contain at least 6 characters");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(50).WithMessage("Name cannot be longer than 50 characters");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(50).WithMessage("Last name cannot be longer than 50 characters");
    }

    // Конструктор для runtime з UserManager
    public RegisterValidator(UserManager<UserEntity> userManager) : this()
    {
        RuleFor(x => x.Email)
            .MustAsync(async (email, cancellation) =>
            {
                var user = await userManager.FindByEmailAsync(email);
                return user == null;
            }).WithMessage("User with this email already exists");
    }
}
