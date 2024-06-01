using FluentValidation;

namespace Workshop.Application.Reservations.Commands.CreateReservation;

public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
{
    public CreateReservationCommandValidator()
    {
        RuleFor(x => x.RentalDate)
            .NotEmpty().WithMessage("Rental date is required")
            .Must(BeAfterCurrentDate).WithMessage("Rental date must be greater than current date")
            .LessThanOrEqualTo(x => x.ReturnDate).WithMessage("Rental cannot be greater than return date");

        RuleFor(x => x.ReturnDate)
            .NotEmpty().WithMessage("Return date is required");

        RuleFor(x => x.CarId)
            .NotEmpty().WithMessage("Car id is required");

        RuleFor(x => x.RentalLocationId)
            .NotEmpty().WithMessage("Rental location id is required");

        RuleFor(x => x.ReturnLocationId)
            .NotEmpty().WithMessage("Return location id is required");
    }

    private bool BeAfterCurrentDate(DateOnly rentalDate)
    {
        return rentalDate > DateOnly.FromDateTime(DateTime.Now);
    }
}