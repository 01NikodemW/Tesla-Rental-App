using MediatR;
using Workshop.Application.Cars.Dtos;

namespace Workshop.Application.Cars.Queries.GetAvailableCars;

public class GetAvailableCarsQuery : IRequest<IEnumerable<CarDto>>
{
    public DateOnly RentalDate { get; set; }
    public DateOnly ReturnDate { get; set; }
}