using MediatR;
using Workshop.Application.Locations.Dtos;

namespace Workshop.Application.Locations.Queries.GetAllLocations;

public class GetAllLocationsQuery : IRequest<IEnumerable<LocationDto>>
{
}