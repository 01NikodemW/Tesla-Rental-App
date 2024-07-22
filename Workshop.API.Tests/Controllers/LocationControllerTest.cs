using System.Threading.Tasks;
using FluentAssertions;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Workshop.API.Controllers;
using Workshop.Domain.Repositories;
using Workshop.Infrastructure.Seeders;
using Xunit;





namespace Workshop.API.Tests.Controllers;

[TestSubject(typeof(LocationController))]
public class LocationControllerTest : IClassFixture<WebApplicationFactory<Program>>
{
    
    private readonly WebApplicationFactory<Program> _factory;
    private readonly Mock<ILocationsRepository> _locationsRepositoryMock = new();
    private readonly Mock<IWorkshopSeeder> _workshopSeederMock = new();

    public LocationControllerTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.Replace(ServiceDescriptor.Scoped(typeof(ILocationsRepository),
                    _ => _locationsRepositoryMock.Object));

                services.Replace(ServiceDescriptor.Scoped(typeof(IWorkshopSeeder),
                    _ => _workshopSeederMock.Object));
            });
        });
    }
    
    [Fact]
    public async Task GetAll_ForValidRequest_Returns200Ok()
    {
        // arrange
        var client = _factory.CreateClient();

        // act
        var result = await client.GetAsync("/api/locations");

        // assert
        result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

}