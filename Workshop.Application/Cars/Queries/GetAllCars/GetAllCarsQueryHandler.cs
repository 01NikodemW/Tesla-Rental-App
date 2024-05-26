using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Workshop.Application.Cars.Dtos;
using Workshop.Domain.Repositories;

namespace Workshop.Application.Cars.Queries.GetAllCars;

public class GetAllCarsQueryHandler(
    ILogger<GetAllCarsQueryHandler> logger,
    ICarsRepository carsRepository,
    IMapper mapper
)
    : IRequestHandler<GetAllCarsQuery, IEnumerable<CarDto>>
{
    public async Task<IEnumerable<CarDto>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all cars");
        var cars = await carsRepository.GetAllAsync();

        var result = mapper.Map<IEnumerable<CarDto>>(cars);
        return result;
    }
}