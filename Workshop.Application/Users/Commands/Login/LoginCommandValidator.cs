using FluentValidation;

namespace Workshop.Application.Users.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(dto => dto.Email)
            .EmailAddress()
            .WithMessage("Please provide a valid email address");

        RuleFor(dto => dto.Password)
            .NotEmpty()
            .WithMessage("Password is required");
    }
}