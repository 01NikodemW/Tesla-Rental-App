using FluentValidation;

namespace Workshop.Application.Cars.Commands.CreateCar;

public class CreateCarCommandValidator : AbstractValidator<CreateCarCommand>
{
    public CreateCarCommandValidator()
    {
        RuleFor(x => x.Brand)
            .MinimumLength(10).WithMessage("The brand must be at least 10 characters long");
    }
}