using MediatR;

namespace Workshop.Application.Users.Commands.RegisterUser;

public class RegisterUserCommand : IRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string PasswordConfirmation { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string CountryId { get; set; }
}