using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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

        services.Configure<AzureBlobStorageSettings>(configuration.GetSection("AzureBlobStorage"));
        services.AddSingleton<BlobServiceClient>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<AzureBlobStorageSettings>>().Value;
            return new BlobServiceClient(options.ConnectionString);
        });


        services.AddDbContext<WorkshopDbContext>(options => { options.UseMySql(connectionString, serverVersion); });


        services.AddScoped<IWorkshopSeeder, WorkshopSeeder>();
        services.AddScoped<ICarsRepository, CarsRepository>();
        services.AddScoped<ICountriesRepository, CountriesRepository>();
        services.AddScoped<ILocationsRepository, LocationsRepository>();
        services.AddScoped<IReservationsRepository, ReservationsRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IBlobStorageRepository, BlobStorageRepository>();
    }
}