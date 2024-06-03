using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Workshop.Domain.Entities;
using Workshop.Domain.Exceptions;
using Workshop.Domain.Repositories;

namespace Workshop.Application.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler(
    ILogger<RegisterUserCommandHandler> logger,
    IUsersRepository usersRepository,
    IPasswordHasher<User> passwordHasher,
    ICountriesRepository countriesRepository
) : IRequestHandler<RegisterUserCommand>
{
    public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("RegisterUserCommandHandler - Handling RegisterUserCommand with email: {Email}",
            request.Email);
        var country = await countriesRepository.GetCountryById(Guid.Parse(request.CountryId), cancellationToken);
        if (country == null)
        {
            throw new NotFoundException(nameof(Country), request.CountryId.ToString());
        }

        var newUser = new User()
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Country = country,
        };


        var passwordHash = passwordHasher.HashPassword(newUser, request.Password);
        newUser.PasswordHash = passwordHash;
        await usersRepository.RegisterUserAsync(newUser, cancellationToken);
    }
}