using MediatR;
using Workshop.Application.Cars.Dtos;

namespace Workshop.Application.Cars.Queries.GetAllCars;

public class GetAllCarsQuery : IRequest<IEnumerable<CarDto>>
{
}