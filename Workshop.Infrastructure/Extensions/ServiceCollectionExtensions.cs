using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Workshop.Domain.Repositories;
using Workshop.Infrastructure.Persistence;
using Workshop.Infrastructure.Repositories;
using Workshop.Infrastructure.Seeders;

namespace Workshop.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("WorkshopDb");
        var serverVersion = ServerVersion.AutoDetect(connectionString);

        services.AddDbContext<WorkshopDbContext>(options =>
        {
            options.UseMySql(connectionString, serverVersion)
                .EnableSensitiveDataLogging();
        });


        services.AddScoped<IWorkshopSeeder, WorkshopSeeder>();
        services.AddScoped<ICarsRepository, CarsRepository>();
        services.AddScoped<ICountriesRepository, CountriesRepository>();
        services.AddScoped<ILocationsRepository, LocationsRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();

        services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = "Bearer";
            option.DefaultScheme = "Bearer";
            option.DefaultChallengeScheme = "Bearer";
        }).AddJwtBearer(cfg =>
        {
            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = true;
            cfg.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = "http://tesla-rental.com",
                ValidAudience = "http://tesla-rental.com",
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YOUR_SECRET_KEY_HERE_32_BYTES_MINIMUM")),
            };
        });
    }
}