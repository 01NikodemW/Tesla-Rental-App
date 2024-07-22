using System;
using FluentValidation.TestHelper;
using JetBrains.Annotations;
using Workshop.Application.Users.Commands.RegisterUser;
using Xunit;
using Moq;
using Workshop.Domain.Repositories;


namespace Workshop.Application.Tests.Users.Commands.RegisterUser;

[TestSubject(typeof(RegisterUserCommandValidator))]
public class RegisterUserCommandValidatorTest
{
    private readonly Mock<IUsersRepository> _usersRepositoryMock;
    private readonly Mock<ICountriesRepository> _countriesRepositoryMock;

    public RegisterUserCommandValidatorTest()
    {
        _usersRepositoryMock = new Mock<IUsersRepository>();
        _countriesRepositoryMock = new Mock<ICountriesRepository>();
    }

    [Fact]
    public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
    {
        // arrange
        _usersRepositoryMock
            .Setup(x => x.CheckIfUserWithProvidedEmailInDb(It.IsAny<string>()))
            .Returns(false);

        _countriesRepositoryMock
            .Setup(x => x.CheckIfCountryWithProvidedIdInDb(It.IsAny<Guid>()))
            .Returns(true);

        var command = new RegisterUserCommand()
        {
            Email = "email@gmail.com",
            Password = "@Haslo123",
            PasswordConfirmation = "@Haslo123",
            FirstName = "John",
            LastName = "Doe",
            CountryId = "8be7088d-a80a-4f9e-b92b-0264f4fec997"
        };

        var validator = new RegisterUserCommandValidator(_usersRepositoryMock.Object, _countriesRepositoryMock.Object);

        // act
        var result = validator.TestValidate(command);

        // assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validator_ForInvalidCommand_ShouldHaveValidationErrors()
    {
        // arrange
        _usersRepositoryMock
            .Setup(x => x.CheckIfUserWithProvidedEmailInDb(It.IsAny<string>()))
            .Returns(false);

        _countriesRepositoryMock
            .Setup(x => x.CheckIfCountryWithProvidedIdInDb(It.IsAny<Guid>()))
            .Returns(true);

        var command = new RegisterUserCommand()
        {
            Email = "emailgmail.com",
            Password = "lo123",
            PasswordConfirmation = "Haslo123",
            FirstName = "J",
            LastName = "D",
            CountryId = "WRONG_GUID"
        };

        var validator = new RegisterUserCommandValidator(_usersRepositoryMock.Object, _countriesRepositoryMock.Object);

        // act
        var result = validator.TestValidate(command);

        // assert
        result.ShouldHaveValidationErrorFor(x => x.Email)
            .WithErrorMessage("Please provide a valid email address");
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorMessage("Password must contain at least 1 capital letter, 1 digit and 1 special character");
        result.ShouldHaveValidationErrorFor(x => x.PasswordConfirmation).WithErrorMessage("Passwords do not match");
        result.ShouldHaveValidationErrorFor(x => x.FirstName)
            .WithErrorMessage("The length of 'First Name' must be 3 - 100 characters");
        result.ShouldHaveValidationErrorFor(x => x.LastName)
            .WithErrorMessage("The length of 'Last Name' must be 3 - 100 characters");
        result.ShouldHaveValidationErrorFor(x => x.CountryId).WithErrorMessage("CountryId must be a valid GUID");
    }

    [Fact]
    public void Validator_ForTakenEmail_ShouldHaveValidationErrors()
    {
        // arrange
        _usersRepositoryMock
            .Setup(x => x.CheckIfUserWithProvidedEmailInDb(It.IsAny<string>()))
            .Returns(true);

        _countriesRepositoryMock
            .Setup(x => x.CheckIfCountryWithProvidedIdInDb(It.IsAny<Guid>()))
            .Returns(true);

        var command = new RegisterUserCommand()
        {
            Email = "email@gmail.com",
            Password = "@Haslo123",
            PasswordConfirmation = "@Haslo123",
            FirstName = "John",
            LastName = "Doe",
            CountryId = "8be7088d-a80a-4f9e-b92b-0264f4fec997"
        };

        var validator = new RegisterUserCommandValidator(_usersRepositoryMock.Object, _countriesRepositoryMock.Object);

        // act
        var result = validator.TestValidate(command);

        // assert
        result.ShouldHaveValidationErrorFor(x => x.Email)
            .WithErrorMessage("Email is taken");
    }

    [Fact]
    public void Validator_ForNotMatchingCountryGuid_ShouldHaveValidationErrors()
    {
        // arrange
        _usersRepositoryMock
            .Setup(x => x.CheckIfUserWithProvidedEmailInDb(It.IsAny<string>()))
            .Returns(false);

        _countriesRepositoryMock
            .Setup(x => x.CheckIfCountryWithProvidedIdInDb(It.IsAny<Guid>()))
            .Returns(false);

        var command = new RegisterUserCommand()
        {
            Email = "email@gmail.com",
            Password = "@Haslo123",
            PasswordConfirmation = "@Haslo123",
            FirstName = "John",
            LastName = "Doe",
            CountryId = "8be7088d-a80a-4f9e-b92b-0264f4fec997"
        };

        var validator = new RegisterUserCommandValidator(_usersRepositoryMock.Object, _countriesRepositoryMock.Object);

        // act
        var result = validator.TestValidate(command);

        // assert
        result.ShouldHaveValidationErrorFor(x => x.CountryId)
            .WithErrorMessage("Country with provided id does not exist");
    }
}