using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Workshop.Domain.Entities;
using Workshop.Domain.Repositories;

namespace Workshop.Application.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler(
    ILogger<RegisterUserCommandHandler> logger,
    IUsersRepository usersRepository,
    IPasswordHasher<User> passwordHasher
) : IRequestHandler<RegisterUserCommand>
{
    public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var newUser = new User()
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
        };
        var passwordHash = passwordHasher.HashPassword(newUser, request.Password);
        newUser.PasswordHash = passwordHash;
        await usersRepository.RegisterUserAsync(newUser);
    }
}