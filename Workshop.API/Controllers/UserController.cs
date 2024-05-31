using MediatR;
using Microsoft.AspNetCore.Mvc;
using Workshop.Application.Users.Commands.Login;
using Workshop.Application.Users.Commands.RegisterUser;

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
}