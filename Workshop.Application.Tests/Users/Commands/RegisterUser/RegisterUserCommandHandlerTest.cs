using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using Workshop.Application.Users.Commands.RegisterUser;
using Workshop.Domain.Entities;
using Workshop.Domain.Repositories;
using Xunit;

namespace Workshop.Application.Tests.Users.Commands.RegisterUser;

[TestSubject(typeof(RegisterUserCommandHandler))]
public class RegisterUserCommandHandlerTest
{
    [Fact]
    public async Task Handle_ForValidCommand_RegisterUser()
    {
        var country = new Country()
        {
            Id = Guid.Parse("8be7088d-a80a-4f9e-b92b-0264f4fec997"),
            Name = "Poland"
        };

        var command = new RegisterUserCommand()
        {
            Email = "email@gmail.com",
            Password = "@Haslo123",
            PasswordConfirmation = "@Haslo123",
            FirstName = "John",
            LastName = "Doe",
            CountryId = "8be7088d-a80a-4f9e-b92b-0264f4fec997"
        };
        //arrange
        var loggerMock = new Mock<ILogger<RegisterUserCommandHandler>>();
        var usersRepositoryMock = new Mock<IUsersRepository>();
        var passwordHasherMock = new Mock<IPasswordHasher<User>>();
        passwordHasherMock.Setup(x => x.HashPassword(It.IsAny<User>(), It.IsAny<string>()))
            .Returns("hashedPassword");
        var countriesRepositoryMock = new Mock<ICountriesRepository>();
        countriesRepositoryMock
            .Setup(x => x.GetCountryById(Guid.Parse(command.CountryId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(country);


        var commandHandler = new RegisterUserCommandHandler(
            loggerMock.Object,
            usersRepositoryMock.Object,
            passwordHasherMock.Object,
            countriesRepositoryMock.Object
        );

        //act
        var act = async () => await commandHandler.Handle(command, CancellationToken.None);

        //assert
        await act.Should().NotThrowAsync<Exception>();
        countriesRepositoryMock.Verify(
            r => r.GetCountryById(Guid.Parse(command.CountryId), It.IsAny<CancellationToken>()),
            Times.Once);


        usersRepositoryMock.Verify(
            r => r.RegisterUserAsync(It.Is<User>(u =>
                u.Email == command.Email &&
                u.FirstName == command.FirstName &&
                u.LastName == command.LastName &&
                u.Country == country &&
                u.PasswordHash == "hashedPassword"
            ), It.IsAny<CancellationToken>()), Times.Once);
    }
}