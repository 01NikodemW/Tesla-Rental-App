using MediatR;

namespace Workshop.Application.Cars.Commands.CreateCar;

public class CreateCarCommand : IRequest<int>
{
    public string Brand { get; set; } = default!;
    public string Model { get; set; } = default!;
}