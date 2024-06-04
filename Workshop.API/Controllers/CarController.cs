using MediatR;
using Microsoft.AspNetCore.Mvc;
using Workshop.Application.Cars.Dtos;
using Workshop.Application.Cars.Queries.GetAllCars;
using Workshop.Application.Cars.Queries.GetAvailableCars;

namespace Workshop.API.Controllers;

[ApiController]
[Route("api/cars")]
public class CarController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CarDto>>> GetAll([FromQuery] GetAllCarsQuery query)
    {
        var cars = await mediator.Send(query);
        return Ok(cars);
    }


    [HttpGet("available")]
    public async Task<ActionResult<IEnumerable<CarDto>>> GetAvailableCars([FromQuery] GetAvailableCarsQuery query)
    {
        var availableCars = await mediator.Send(query);
        return Ok(availableCars);
    }
}