using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Workshop.Application.Extensions;
using Workshop.Application.Users.Dtos;
using Workshop.Domain.Entities;
using Workshop.Domain.Exceptions;
using Workshop.Domain.Repositories;


namespace Workshop.Application.Users.Commands.Login;

public class LoginCommandHandler(
    ILogger<LoginCommandHandler> logger,
    IUsersRepository usersRepository,
    IPasswordHasher<User> passwordHasher,
    IOptions<AuthenticationSettings> options)
    : IRequestHandler<LoginCommand, LoginResponse>
{
    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("LoginCommandHandler - Handling LoginCommand with email: {Email}", request.Email);
        var user = await usersRepository.GetUserByEmailAsync(request.Email, cancellationToken);
        if (user is null)
        {
            throw new UnauthorizedException();
        }

        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (result == PasswordVerificationResult.Failed)
        {
            throw new UnauthorizedException();
        }

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email.ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.JwtKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(options.Value.JwtExpireDays);

        var token = new JwtSecurityToken(options.Value.JwtIssuer,
            options.Value.JwtAudience,
            claims,
            expires: expires,
            signingCredentials: cred);

        var tokenHandler = new JwtSecurityTokenHandler();
        var generatedToken = tokenHandler.WriteToken(token);
        return new LoginResponse(generatedToken);
    }
}