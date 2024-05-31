using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Workshop.Domain.Entities;

namespace Workshop.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
        services.AddAutoMapper(applicationAssembly);
        services.AddValidatorsFromAssembly(applicationAssembly)
            .AddFluentValidationAutoValidation();

        services.AddHttpContextAccessor();


        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
    }
}