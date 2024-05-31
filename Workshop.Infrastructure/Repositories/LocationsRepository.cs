using Microsoft.EntityFrameworkCore;
using Workshop.Domain.Entities;
using Workshop.Domain.Repositories;
using Workshop.Infrastructure.Persistence;

namespace Workshop.Infrastructure.Repositories;

internal class LocationsRepository(WorkshopDbContext dbContext) : ILocationsRepository
{
    public async Task<IEnumerable<Location>> GetAllLocations()
    {
        var locations = await dbContext.Locations.ToListAsync();
        return locations;
    }
}