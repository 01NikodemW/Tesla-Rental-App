using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workshop.Application.Reservations.Commands.CreateReservation;
using Workshop.Application.Reservations.Dtos;
using Workshop.Application.Users.Commands.Login;
using Workshop.Application.Users.Commands.RegisterUser;
using Workshop.Application.Users.Queries.GetUserReservations;

namespace Workshop.API.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser([FromBody] RegisterUserCommand command)
    {
        await mediator.Send(command);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginCommand command)
    {
        var token = await mediator.Send(command);
        return Ok(token);
    }

    [Authorize]
    [HttpGet("reservations")]
    public async Task<ActionResult<ReservationDto>> GetUserReservations([FromQuery] GetUserReservationsQuery command)
    {
        var userIdString = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        if (Guid.TryParse(userIdString, out var userId))
        {
            command.UserId = userId;
            var id = await mediator.Send(command);
            return Ok(id);
        }

        return BadRequest("Invalid user ID");
    }
}