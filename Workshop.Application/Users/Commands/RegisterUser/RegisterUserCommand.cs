using MediatR;

namespace Workshop.Application.Users.Commands.RegisterUser;

public class RegisterUserCommand : IRequest
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string PasswordConfirmation { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string CountryId { get; set; } = default!;
}