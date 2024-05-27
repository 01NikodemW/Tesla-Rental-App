using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workshop.Application.Cars.Commands.CreateCar;
using Workshop.Application.Cars.Dtos;
using Workshop.Application.Cars.Queries.GetAllCars;

namespace Workshop.API.Controllers;

[ApiController]
[Route("api/cars")]
public class CarController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<CarDto>>> GetAll([FromQuery] GetAllCarsQuery query)
    {
        var restaurants = await mediator.Send(query);
        return Ok(restaurants);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateCarCommand command)
    {
        var id = await mediator.Send(command);
        return Ok(id);
    }
}