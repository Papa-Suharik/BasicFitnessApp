using BasicFitnessApp.Dto;
using FluentValidation;

namespace BasicFitnessApp.Validators;
public class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(x => x.Password)    
            .NotEmpty().WithMessage("Password is required")
            .Length(8).WithMessage("Password should be at least 8 characters long");
    }
}