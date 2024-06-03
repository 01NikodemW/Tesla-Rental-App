using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Workshop.Application.Locations.Dtos;
using Workshop.Domain.Repositories;

namespace Workshop.Application.Locations.Queries.GetAllLocations;

public class GetAllLocationsQueryHandler(
    ILogger<GetAllLocationsQuery> logger,
    ILocationsRepository locationsRepository,
    IMapper mapper) : IRequestHandler<GetAllLocationsQuery, IEnumerable<LocationDto>>
{
    public async Task<IEnumerable<LocationDto>> Handle(GetAllLocationsQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all locations");
        var locations = await locationsRepository.GetAllLocations(cancellationToken);
        var result = mapper.Map<IEnumerable<LocationDto>>(locations);
        return result;
    }
}