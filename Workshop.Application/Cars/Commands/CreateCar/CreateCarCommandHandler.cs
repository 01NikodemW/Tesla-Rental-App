using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Workshop.Domain.Entities;
using Workshop.Domain.Repositories;

namespace Workshop.Application.Cars.Commands.CreateCar;

public class CreateCarCommandHandler(
    ILogger<CreateCarCommandHandler> logger,
    ICarsRepository carsRepository,
    IMapper mapper
) : IRequestHandler<CreateCarCommand, int>
{
    public async Task<int> Handle(CreateCarCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateCarCommandHandler");

        var car = mapper.Map<Car>(request);

        var id = await carsRepository.CreateAsync(car);

        return id;
    }
}