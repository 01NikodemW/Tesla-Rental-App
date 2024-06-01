using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Workshop.Application.Cars.Dtos;
using Workshop.Domain.Repositories;

namespace Workshop.Application.Cars.Queries.GetAvailableCars;

public class GetAvailableCarsQueryHandler(
    ILogger<GetAvailableCarsQueryHandler> logger,
    ICarsRepository carsRepository,
    IMapper mapper
) : IRequestHandler<GetAvailableCarsQuery, IEnumerable<CarDto>>
{
    public async Task<IEnumerable<CarDto>> Handle(GetAvailableCarsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting available cars for rental date: {RentalDate} and return date: {ReturnDate}",
            request.RentalDate,
            request.ReturnDate
        );
        var cars = await carsRepository.GetAvailableCars(request.RentalDate, request.ReturnDate);

        var result = mapper.Map<IEnumerable<CarDto>>(cars);
        return result;
    }
}