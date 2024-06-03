using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workshop.Application.Cars.Commands.CreateCar;
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

    // [HttpPost]
    // public async Task<ActionResult<int>> Create([FromBody] CreateCarCommand command)
    // {
    //     var id = await mediator.Send(command);
    //     return Ok(id);
    // }

    [HttpGet("available")]
    public async Task<ActionResult<IEnumerable<CarDto>>> GetAvailableCars([FromQuery] GetAvailableCarsQuery query)
    {
        var availableCars = await mediator.Send(query);
        return Ok(availableCars);
    }
}