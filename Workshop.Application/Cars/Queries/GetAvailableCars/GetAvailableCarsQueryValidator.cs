using FluentValidation;

namespace Workshop.Application.Cars.Queries.GetAvailableCars;

public class GetAvailableCarsQueryValidator : AbstractValidator<GetAvailableCarsQuery>
{
    public GetAvailableCarsQueryValidator()
    {
        RuleFor(x => x.RentalDate)
            .NotEmpty().WithMessage("Rental date is required")
            .Must(BeAfterCurrentDate).WithMessage("Rental date must be greater than current date")
            .LessThanOrEqualTo(x => x.ReturnDate).WithMessage("Rental cannot be greater than return date");

        RuleFor(x => x.ReturnDate)
            .NotEmpty().WithMessage("Return date is required");
    }

    private bool BeAfterCurrentDate(DateOnly rentalDate)
    {
        return rentalDate > DateOnly.FromDateTime(DateTime.Now);
    }
}