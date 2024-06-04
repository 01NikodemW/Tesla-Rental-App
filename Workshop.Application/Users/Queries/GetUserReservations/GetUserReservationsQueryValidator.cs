using FluentValidation;
using Workshop.Application.Cars.Queries.GetAvailableCars;

namespace Workshop.Application.Users.Queries.GetUserReservations;

public class GetUserReservationsQueryValidator : AbstractValidator<GetUserReservationsQuery>
{
    public GetUserReservationsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("Page number must be greater than 0");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("Page size must be greater than 0");
    }
}