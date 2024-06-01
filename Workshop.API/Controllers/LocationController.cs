using MediatR;
using Microsoft.AspNetCore.Mvc;
using Workshop.Application.Locations.Dtos;
using Workshop.Application.Locations.Queries.GetAllLocations;

namespace Workshop.API.Controllers;

[ApiController]
[Route("api/locations")]
public class LocationController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LocationDto>>> GetAll([FromQuery] GetAllLocationsQuery query)
    {
        var locations = await mediator.Send(query);
        return Ok(locations);
    }
}